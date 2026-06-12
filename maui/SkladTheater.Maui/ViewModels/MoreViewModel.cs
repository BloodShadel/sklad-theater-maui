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
            new() { Title = "Предметы", Href = "items.html", Route = "//MainPage" },
            new() { Title = "Задания", Href = "tasks.html", Route = "//MainPage" },
            new() { Title = "Сундуки", Href = "chests.html", Route = "//MainPage" },
            new() { Title = "Стойла", Href = "stalls.html", Route = "//MainPage" },
            new() { Title = "Скотч", Href = "tape.html", Route = "//MainPage" },
            new() { Title = "Спектакли", Href = "shows.html", Route = "//MainPage" },
            new() { Title = "Кассеты", Href = "kassety.html", Route = "//MainPage" },
            new() { Title = "График", Href = "grafik.html", Route = "//MainPage" },
            new() { Title = "Краснопахрская", Href = "krano.html", Route = "//MainPage" },
            new() { Title = "История", Href = "history.html", Route = "//MainPage" },
            new() { Title = "Роли", Href = "roles.html", Route = "//MainPage" },
            new() { Title = "Развеска", Href = "razveska.html", Route = "//MainPage" },
            new() { Title = "Персонал", Href = "users.html", Route = "//MainPage" },
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
        await Shell.Current.GoToAsync(item.Route);
    }
}
