using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public partial class KranoViewModel : ObservableObject
{
    private readonly ISkladApiService _api;

    [ObservableProperty]
    private ObservableCollection<Chest> _preparing = new();

    [ObservableProperty]
    private ObservableCollection<Chest> _chests = new();

    [ObservableProperty]
    private ObservableCollection<ItemDto> _items = new();

    [ObservableProperty]
    private int _selectedTab;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public KranoViewModel(ISkladApiService api)
    {
        _api = api;
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsBusy = true;
        try
        {
            var preparing = await _api.GetChestsByLocationAsync("preparing");
            var mainChests = await _api.GetChestsByLocationAsync("main");
            var allItems = await _api.GetItemsAsync();
            var kranoItems = allItems.Where(i => i.Location == "krano").ToList();

            UpdateCollection(Preparing, preparing);
            UpdateCollection(Chests, mainChests);
            UpdateCollection(Items, kranoItems);
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

    private static void UpdateCollection<T>(ObservableCollection<T> collection, List<T> items)
    {
        collection.Clear();
        foreach (var item in items)
            collection.Add(item);
    }
}
