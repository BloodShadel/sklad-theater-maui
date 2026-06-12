using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkladTheater.Maui.Helpers;
using SkladTheater.Maui.Models;
using System.Net.Security;
using System.Net.WebSockets;
using System.Text;

namespace SkladTheater.Maui.Services;

public interface IWebSocketService
{
    Task ConnectAsync(int userId);
    Task DisconnectAsync();
    Task SendTypingAsync(int chatId, List<int> participantIds, string userName);
    bool IsConnected { get; }
}

public class WebSocketService : IWebSocketService
{
    private readonly ITokenService _tokenService;
    private readonly IPreferenceService _prefs;
    private readonly WeakReferenceMessenger _messenger;

    private ClientWebSocket? _socket;
    private CancellationTokenSource? _cts;
    private int _userId;
    private bool _shouldReconnect;

    public bool IsConnected => _socket?.State == WebSocketState.Open;

    public WebSocketService(ITokenService tokenService, IPreferenceService prefs)
    {
        _tokenService = tokenService;
        _prefs = prefs;
        _messenger = WeakReferenceMessenger.Default;
    }

    public async Task ConnectAsync(int userId)
    {
        _userId = userId;
        _shouldReconnect = true;
        await TryConnectAsync();
    }

    private async Task TryConnectAsync()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        try
        {
            _socket?.Dispose();
            _socket = new ClientWebSocket();

            var token = await _tokenService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _socket.Options.SetRequestHeader("Authorization", $"Bearer {token}");
            }

            var handler = new SocketsHttpHandler
            {
                SslOptions = new SslClientAuthenticationOptions
                {
                    RemoteCertificateValidationCallback = (sender, cert, chain, errors) => true
                }
            };

            var uri = new Uri($"{AppConfig.WebSocketUrl}?userId={_userId}");
            await _socket.ConnectAsync(uri, new HttpMessageInvoker(handler), _cts.Token);

            _ = ReceiveLoopAsync(_cts.Token);
            _ = PingLoopAsync(_cts.Token);
        }
        catch
        {
            _ = ScheduleReconnectAsync();
        }
    }

    public async Task DisconnectAsync()
    {
        _shouldReconnect = false;
        _cts?.Cancel();
        if (_socket?.State == WebSocketState.Open)
        {
            try { await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "logout", CancellationToken.None); } catch { }
        }
        _socket?.Dispose();
        _socket = null;
    }

    public async Task SendTypingAsync(int chatId, List<int> participantIds, string userName)
    {
        if (_socket?.State != WebSocketState.Open) return;
        var payload = JsonConvert.SerializeObject(new { type = "typing", chatId, participantIds, userName });
        var bytes = Encoding.UTF8.GetBytes(payload);
        await _socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, _cts?.Token ?? default);
    }

    private async Task ReceiveLoopAsync(CancellationToken ct)
    {
        var buffer = new byte[4096];
        try
        {
            while (_socket?.State == WebSocketState.Open && !ct.IsCancellationRequested)
            {
                var result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), ct);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "closed", CancellationToken.None);
                    break;
                }
                var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                HandleMessage(json);
            }
        }
        catch { }
        finally
        {
            if (_shouldReconnect) _ = ScheduleReconnectAsync();
        }
    }

    private async Task PingLoopAsync(CancellationToken ct)
    {
        try
        {
            while (!ct.IsCancellationRequested && _socket?.State == WebSocketState.Open)
            {
                await Task.Delay(TimeSpan.FromSeconds(30), ct);
                if (_socket?.State == WebSocketState.Open)
                {
                    var bytes = Encoding.UTF8.GetBytes("{\"type\":\"ping\"}");
                    await _socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, ct);
                }
            }
        }
        catch { }
    }

    private async Task ScheduleReconnectAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        if (_shouldReconnect) await TryConnectAsync();
    }

    private void HandleMessage(string json)
    {
        try
        {
            var obj = JObject.Parse(json);
            var type = obj["type"]?.ToString();
            switch (type)
            {
                case "new_task":
                    var task = obj["task"]?.ToObject<TaskDto>();
                    if (task != null) _messenger.Send(new NewTaskMessage { Task = task });
                    break;
                case "chat_message":
                    var msg = obj["message"]?.ToObject<ChatMessageDto>();
                    if (msg != null) _messenger.Send(new ChatMessageReceived { Message = msg });
                    break;
                case "messages_read":
                    _messenger.Send(new MessagesReadReceived
                    {
                        ChatId = obj["chatId"]?.Value<int>() ?? 0,
                        ReadBy = obj["readBy"]?.Value<int>() ?? 0,
                        MessageIds = obj["messageIds"]?.ToObject<List<int>>() ?? new List<int>()
                    });
                    break;
                case "online_users":
                    _messenger.Send(new OnlineUsersReceived
                    {
                        UserIds = obj["userIds"]?.ToObject<List<int>>() ?? new List<int>()
                    });
                    break;
                case "user_online":
                    _messenger.Send(new UserOnlineReceived { UserId = obj["userId"]?.Value<int>() ?? 0 });
                    break;
                case "user_offline":
                    _messenger.Send(new UserOfflineReceived { UserId = obj["userId"]?.Value<int>() ?? 0 });
                    break;
                case "typing":
                    _messenger.Send(new TypingReceived
                    {
                        ChatId = obj["chatId"]?.Value<int>() ?? 0,
                        UserId = obj["userId"]?.Value<int>() ?? 0,
                        UserName = obj["userName"]?.ToString() ?? string.Empty
                    });
                    break;
                case "message_edited":
                    var edited = obj["message"]?.ToObject<ChatMessageDto>();
                    if (edited != null) _messenger.Send(new MessageEditedReceived { Message = edited });
                    break;
                case "message_deleted":
                    _messenger.Send(new MessageDeletedReceived
                    {
                        ChatId = obj["chatId"]?.Value<int>() ?? 0,
                        MessageId = obj["messageId"]?.Value<int>() ?? 0
                    });
                    break;
                case "message_reaction":
                    var reacted = obj["message"]?.ToObject<ChatMessageDto>();
                    if (reacted != null) _messenger.Send(new MessageEditedReceived { Message = reacted });
                    break;
                case "chat_pin":
                    _messenger.Send(new PinChangedReceived
                    {
                        ChatId = obj["chatId"]?.Value<int>() ?? 0,
                        MessageId = obj["messageId"]?.Value<int?>()
                    });
                    break;
                case "user_left_chat":
                case "member_added":
                case "member_removed":
                case "chat_renamed":
                    _messenger.Send(new ChatListUpdateReceived());
                    break;
                case "data_changed":
                    _messenger.Send(new DataChangedReceived { Collection = obj["collection"]?.ToString() ?? string.Empty });
                    break;
            }
        }
        catch { }
    }
}
