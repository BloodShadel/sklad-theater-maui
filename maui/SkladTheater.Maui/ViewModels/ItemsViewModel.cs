using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public partial class ItemsViewModel : ObservableObject
{
    private readonly ISkladApiService _api;
    private readonly IOfflineCacheService _cache;

    [ObservableProperty]
    private ObservableCollection<ItemDto> _items = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _isOffline;

    private readonly IAuthService _authService;

    public ItemsViewModel(ISkladApiService api, IOfflineCacheService cache, IAuthService authService)
    {
        _api = api;
        _cache = cache;
        _authService = authService;
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsBusy = true;
        StatusMessage = string.Empty;

        try
        {
            var items = await _api.GetItemsAsync();
            _cache.SaveItems(items);
            UpdateItems(items);
            IsOffline = false;
        }
        catch (Exception ex)
        {
            var cached = _cache.GetItems();
            if (cached != null)
            {
                UpdateItems(cached);
                IsOffline = true;
                StatusMessage = "Нет связи. Показаны сохранённые данные.";
            }
            else
            {
                StatusMessage = $"Ошибка загрузки: {ex.Message}";
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    partial void OnSearchTextChanged(string value) => FilterItems();

    private void UpdateItems(List<ItemDto> items)
    {
        Items.Clear();
        foreach (var item in items)
            Items.Add(item);
        FilterItems();
    }

    private void FilterItems()
    {
        // UI filtering is handled by CollectionView if we bind ItemsSource and use Text property
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
