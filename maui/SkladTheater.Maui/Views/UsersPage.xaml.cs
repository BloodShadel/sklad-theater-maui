using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class UsersPage : ContentPage
{
    public UsersPage(UsersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is UsersViewModel vm && vm.Users.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
