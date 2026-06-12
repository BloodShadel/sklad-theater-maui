using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public partial class StallsViewModel : ObservableObject
{
    private readonly ISkladApiService _api;
    private readonly IOfflineCacheService _cache;

    [ObservableProperty]
    private ObservableCollection<StallDto> _stalls = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public StallsViewModel(ISkladApiService api, IOfflineCacheService cache)
    {
        _api = api;
        _cache = cache;
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsBusy = true;
        try
        {
            var items = await _api.GetStallsAsync();
            _cache.SaveStalls(items);
            UpdateStalls(items);
        }
        catch (Exception ex)
        {
            var cached = _cache.GetStalls();
            if (cached != null)
                UpdateStalls(cached);
            else
                StatusMessage = $"Ошибка: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateStalls(List<StallDto> stalls)
    {
        Stalls.Clear();
        foreach (var s in stalls)
            Stalls.Add(s);
    }
}
