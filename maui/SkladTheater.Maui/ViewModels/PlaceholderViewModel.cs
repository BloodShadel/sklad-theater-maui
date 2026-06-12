using CommunityToolkit.Mvvm.ComponentModel;

namespace SkladTheater.Maui.ViewModels;

[QueryProperty(nameof(Title), "title")]
[QueryProperty(nameof(Message), "message")]
public partial class PlaceholderViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "Раздел";

    [ObservableProperty]
    private string _message = "В разработке";
}
