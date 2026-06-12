using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public partial class TasksViewModel : ObservableObject
{
    private readonly ISkladApiService _api;
    private readonly IOfflineCacheService _cache;

    [ObservableProperty]
    private ObservableCollection<TaskDto> _tasks = new();

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _isOffline;

    public TasksViewModel(ISkladApiService api, IOfflineCacheService cache)
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
            var items = await _api.GetTasksAsync();
            _cache.SaveTasks(items);
            UpdateTasks(items);
            IsOffline = false;
        }
        catch (Exception ex)
        {
            var cached = _cache.GetTasks();
            if (cached != null)
            {
                UpdateTasks(cached);
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

    private void UpdateTasks(List<TaskDto> tasks)
    {
        Tasks.Clear();
        foreach (var t in tasks.Where(t => t.Status != "completed" && t.Status != "cancelled"))
            Tasks.Add(t);
    }

    [RelayCommand]
    private async Task CompleteAsync(TaskDto? task)
    {
        if (task == null) return;
        try
        {
            await _api.UpdateTaskStatusAsync(task.Id, "completed", null);
            await LoadAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Ошибка: {ex.Message}";
        }
    }
}
