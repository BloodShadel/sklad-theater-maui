using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class StallsPage : ContentPage
{
    public StallsPage(StallsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is StallsViewModel vm && vm.Stalls.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
