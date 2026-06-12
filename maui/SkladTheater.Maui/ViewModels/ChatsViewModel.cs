using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public partial class ChatsViewModel : ObservableObject
{
    private readonly ISkladApiService _api;
    private readonly IOfflineCacheService _cache;

    [ObservableProperty]
    private ObservableCollection<ChatDto> _chats = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public ChatsViewModel(ISkladApiService api, IOfflineCacheService cache)
    {
        _api = api;
        _cache = cache;
        WeakReferenceMessenger.Default.Register<ChatMessageReceived>(this, OnNewMessage);
        WeakReferenceMessenger.Default.Register<ChatListUpdateReceived>(this, (_, __) => MainThread.BeginInvokeOnMainThread(async () => await LoadAsync()));
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsBusy = true;
        StatusMessage = string.Empty;
        try
        {
            var items = await _api.GetChatsAsync();
            _cache.SaveChats(items);
            UpdateChats(items);
        }
        catch (Exception ex)
        {
            var cached = _cache.GetChats();
            if (cached != null)
                UpdateChats(cached);
            else
                StatusMessage = $"Ошибка: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateChats(List<ChatDto> chats)
    {
        Chats.Clear();
        foreach (var c in chats.OrderByDescending(c => c.UpdatedAt))
            Chats.Add(c);
    }

    private void OnNewMessage(object recipient, ChatMessageReceived msg)
    {
        MainThread.BeginInvokeOnMainThread(async () => await LoadAsync());
    }

    [RelayCommand]
    private async Task OpenChatAsync(ChatDto? chat)
    {
        if (chat == null) return;
        await Shell.Current.GoToAsync($"ChatRoomPage?chatId={chat.Id}&chatName={Uri.EscapeDataString(chat.DisplayName)}");
    }
}
