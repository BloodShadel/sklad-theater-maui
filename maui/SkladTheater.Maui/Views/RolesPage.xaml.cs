using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class RolesPage : ContentPage
{
    public RolesPage(RolesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is RolesViewModel vm && vm.Roles.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
