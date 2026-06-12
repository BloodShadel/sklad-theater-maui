using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public partial class ShowsViewModel : ObservableObject
{
    private readonly ISkladApiService _api;
    private readonly IOfflineCacheService _cache;

    [ObservableProperty]
    private ObservableCollection<ShowDto> _shows = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public ShowsViewModel(ISkladApiService api, IOfflineCacheService cache)
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
            var items = await _api.GetShowsAsync();
            _cache.SaveShows(items);
            UpdateShows(items);
        }
        catch (Exception ex)
        {
            var cached = _cache.GetShows();
            if (cached != null)
                UpdateShows(cached);
            else
                StatusMessage = $"Ошибка: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateShows(List<ShowDto> shows)
    {
        Shows.Clear();
        foreach (var s in shows)
            Shows.Add(s);
    }
}
