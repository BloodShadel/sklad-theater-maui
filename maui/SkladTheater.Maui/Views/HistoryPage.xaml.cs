using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class HistoryPage : ContentPage
{
    public HistoryPage(HistoryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HistoryViewModel vm && vm.History.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
