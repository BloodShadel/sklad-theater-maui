using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public partial class ChestsViewModel : ObservableObject
{
    private readonly ISkladApiService _api;
    private readonly IOfflineCacheService _cache;

    [ObservableProperty]
    private ObservableCollection<Chest> _chests = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _isOffline;

    public ChestsViewModel(ISkladApiService api, IOfflineCacheService cache)
    {
        _api = api;
        _cache = cache;
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsBusy = true;
        StatusMessage = string.Empty;
        try
        {
            var items = await _api.GetChestsAsync();
            _cache.SaveChests(items);
            UpdateChests(items);
            IsOffline = false;
        }
        catch (Exception ex)
        {
            var cached = _cache.GetChests();
            if (cached != null)
            {
                UpdateChests(cached);
                IsOffline = true;
                StatusMessage = "Нет связи. Показаны сохранённые данные.";
            }
            else
            {
                StatusMessage = $"Ошибка: {ex.Message}";
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateChests(List<Chest> chests)
    {
        Chests.Clear();
        foreach (var c in chests)
            Chests.Add(c);
    }
}
