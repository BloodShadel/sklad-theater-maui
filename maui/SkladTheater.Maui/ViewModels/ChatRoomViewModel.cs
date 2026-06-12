using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

[QueryProperty(nameof(ChatId), "chatId")]
[QueryProperty(nameof(ChatName), "chatName")]
public partial class ChatRoomViewModel : ObservableObject, IQueryAttributable
{
    private readonly ISkladApiService _api;
    private readonly IAuthService _authService;

    [ObservableProperty]
    private int _chatId;

    [ObservableProperty]
    private string _chatName = string.Empty;

    [ObservableProperty]
    private string _messageText = string.Empty;

    [ObservableProperty]
    private ObservableCollection<ChatMessageDto> _messages = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    private int _currentUserId;

    public ChatRoomViewModel(ISkladApiService api, IAuthService authService)
    {
        _api = api;
        _authService = authService;
        WeakReferenceMessenger.Default.Register<ChatMessageReceived>(this, OnMessageReceived);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("chatId", out var cid) && int.TryParse(cid.ToString(), out var id))
            ChatId = id;
        if (query.TryGetValue("chatName", out var name))
            ChatName = Uri.UnescapeDataString(name?.ToString() ?? string.Empty);

        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        _currentUserId = user != null ? (int)user.Id : 0;
        await LoadAsync();
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (ChatId <= 0) return;
        IsBusy = true;
        try
        {
            var msgs = await _api.GetMessagesAsync(ChatId, limit: 100);
            Messages.Clear();
            foreach (var m in msgs)
                Messages.Add(m);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Ошибка: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SendAsync()
    {
        if (ChatId <= 0 || string.IsNullOrWhiteSpace(MessageText)) return;
        try
        {
            var sent = await _api.SendMessageAsync(ChatId, MessageText.Trim(), null);
            if (sent != null)
            {
                Messages.Add(sent);
                MessageText = string.Empty;
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Ошибка отправки: {ex.Message}";
        }
    }

    private void OnMessageReceived(object recipient, ChatMessageReceived msg)
    {
        if (msg.Message.ChatId != ChatId) return;
        MainThread.BeginInvokeOnMainThread(() => Messages.Add(msg.Message));
    }
}
