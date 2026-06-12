using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SkladTheater.Maui.Helpers;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;

namespace SkladTheater.Maui.ViewModels;

[QueryProperty(nameof(ChatId), "chatId")]
[QueryProperty(nameof(ChatName), "chatName")]
public partial class ChatRoomViewModel : ObservableObject, IRecipient<ChatMessageReceived>
{
    private readonly ISkladApiService _api;

    [ObservableProperty]
    private int _chatId;

    [ObservableProperty]
    private string _chatName = string.Empty;

    [ObservableProperty]
    private string _messageText = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private ObservableCollection<ChatMessageDto> _messages = new();

    public ChatRoomViewModel(ISkladApiService api)
    {
        _api = api;
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(ChatMessageReceived message)
    {
        if (message.Message.ChatId != ChatId) return;
        ResolveAvatar(message.Message);
        MainThread.BeginInvokeOnMainThread(() => Messages.Add(message.Message));
    }

    public async Task LoadMessagesAsync()
    {
        IsBusy = true;
        try
        {
            var list = await _api.GetMessagesAsync(ChatId);
            foreach (var m in list) ResolveAvatar(m);
            Messages = new ObservableCollection<ChatMessageDto>(list);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private static void ResolveAvatar(ChatMessageDto message)
    {
        message.SenderAvatarUrl = ImageUrlHelper.Resolve(message.SenderAvatarUrl);
    }

    [RelayCommand]
    private async Task SendAsync()
    {
        if (string.IsNullOrWhiteSpace(MessageText)) return;

        IsBusy = true;
        try
        {
            var sent = await _api.SendMessageAsync(ChatId, MessageText.Trim(), null);
            if (sent != null)
            {
                ResolveAvatar(sent);
                Messages.Add(sent);
                MessageText = string.Empty;
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка", "Не удалось отправить сообщение", "OK");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}
