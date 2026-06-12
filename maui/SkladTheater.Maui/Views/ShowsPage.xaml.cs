using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class ShowsPage : ContentPage
{
    public ShowsPage(ShowsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ShowsViewModel vm && vm.Shows.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
