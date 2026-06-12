using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public class ShiftRow
{
    public string UserName { get; set; } = string.Empty;
    public string Shifts { get; set; } = string.Empty;
}

public partial class GrafikViewModel : ObservableObject
{
    private readonly ISkladApiService _api;
    private readonly IOfflineCacheService _cache;

    [ObservableProperty]
    private ObservableCollection<ShiftRow> _rows = new();

    [ObservableProperty]
    private string _month = DateTime.Now.ToString("yyyy-MM");

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _canEdit;

    public GrafikViewModel(ISkladApiService api, IOfflineCacheService cache, IPermissionService permissions)
    {
        _api = api;
        _cache = cache;
        CanEdit = permissions.IsManagerOrAdmin();
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsBusy = true;
        try
        {
            var response = await _api.GetScheduleAsync(Month);
            if (response != null)
            {
                _cache.SaveGrafik(Month, response.Schedule);
                BuildRows(response.Schedule);
            }
        }
        catch (Exception ex)
        {
            var cached = _cache.GetGrafik(Month);
            if (cached != null)
                BuildRows(cached);
            else
                StatusMessage = $"Ошибка: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void BuildRows(ScheduleMonth schedule)
    {
        Rows.Clear();
        foreach (var plan in schedule.Plans)
        {
            var row = new ShiftRow
            {
                UserName = plan.Value,
                Shifts = string.Join(", ", schedule.Shifts.Where(s => s.Key.StartsWith(plan.Key + "_")).Select(s => $"{s.Key.Split('_').Last()}: {s.Value}"))
            };
            Rows.Add(row);
        }
    }
}
