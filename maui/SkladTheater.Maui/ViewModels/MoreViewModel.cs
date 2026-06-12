using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Services;
using System.Collections.ObjectModel;

namespace SkladTheater.Maui.ViewModels;

public class MenuItem
{
    public string Title { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
}

public partial class MoreViewModel : ObservableObject
{
    private readonly IPermissionService _permissions;

    [ObservableProperty]
    private ObservableCollection<MenuItem> _menuItems = new();

    public MoreViewModel(IPermissionService permissions)
    {
        _permissions = permissions;
    }

    [RelayCommand]
    private void Load()
    {
        var all = new List<MenuItem>
        {
            new() { Title = "Предметы", Href = "items.html", Route = "ItemsPage" },
            new() { Title = "Задания", Href = "tasks.html", Route = "TasksPage" },
            new() { Title = "Сундуки", Href = "chests.html", Route = "ChestsPage" },
            new() { Title = "Стойла", Href = "stalls.html", Route = "StallsPage" },
            new() { Title = "Скотч", Href = "tape.html", Route = "TapePage" },
            new() { Title = "Спектакли", Href = "shows.html", Route = "ShowsPage" },
            new() { Title = "Кассеты", Href = "kassety.html", Route = "KassetyPage" },
            new() { Title = "График", Href = "grafik.html", Route = "PlaceholderPage" },
            new() { Title = "Краснопахрская", Href = "krano.html", Route = "PlaceholderPage" },
            new() { Title = "История", Href = "history.html", Route = "PlaceholderPage" },
            new() { Title = "Роли", Href = "roles.html", Route = "PlaceholderPage" },
            new() { Title = "Развеска", Href = "razveska.html", Route = "PlaceholderPage" },
            new() { Title = "Персонал", Href = "users.html", Route = "PlaceholderPage" },
            new() { Title = "Профиль", Href = "profile.html", Route = "ProfilePage" },
        };

        MenuItems.Clear();
        foreach (var item in all)
        {
            if (_permissions.HasPermission(item.Href))
                MenuItems.Add(item);
        }
    }

    [RelayCommand]
    private async Task NavigateAsync(MenuItem? item)
    {
        if (item == null) return;
        if (item.Route == "PlaceholderPage")
        {
            await Shell.Current.GoToAsync($"PlaceholderPage?title={Uri.EscapeDataString(item.Title)}&message={Uri.EscapeDataString("Раздел в разработке")}");
            return;
        }
        await Shell.Current.GoToAsync(item.Route);
    }
}
