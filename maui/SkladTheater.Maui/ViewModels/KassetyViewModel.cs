using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public partial class KassetyViewModel : ObservableObject
{
    private readonly ISkladApiService _api;
    private readonly IOfflineCacheService _cache;

    [ObservableProperty]
    private ObservableCollection<KassetaDto> _kassety = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public KassetyViewModel(ISkladApiService api, IOfflineCacheService cache)
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
            var items = await _api.GetKassetyAsync();
            _cache.SaveKassety(items);
            UpdateKassety(items);
        }
        catch (Exception ex)
        {
            var cached = _cache.GetKassety();
            if (cached != null)
                UpdateKassety(cached);
            else
                StatusMessage = $"Ошибка: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateKassety(List<KassetaDto> kassety)
    {
        Kassety.Clear();
        foreach (var k in kassety)
            Kassety.Add(k);
    }
}
