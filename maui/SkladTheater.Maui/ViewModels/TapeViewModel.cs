using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public partial class TapeViewModel : ObservableObject
{
    private readonly ISkladApiService _api;
    private readonly IOfflineCacheService _cache;

    [ObservableProperty]
    private ObservableCollection<TapeDto> _tapes = new();

    [ObservableProperty]
    private SeasonStats _stats = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public TapeViewModel(ISkladApiService api, IOfflineCacheService cache)
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
            var response = await _api.GetTapesAsync();
            if (response != null)
            {
                _cache.SaveTapes(response.Tapes, response.SeasonStats);
                UpdateTapes(response.Tapes);
                Stats = response.SeasonStats;
            }
        }
        catch (Exception ex)
        {
            var cached = _cache.GetTapes();
            if (cached.Tapes != null)
            {
                UpdateTapes(cached.Tapes);
                Stats = cached.Stats ?? new SeasonStats();
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

    private void UpdateTapes(List<TapeDto> tapes)
    {
        Tapes.Clear();
        foreach (var t in tapes)
            Tapes.Add(t);
    }
}
