using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class ChestsPage : ContentPage
{
    public ChestsPage(ChestsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ChestsViewModel vm && vm.Chests.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
