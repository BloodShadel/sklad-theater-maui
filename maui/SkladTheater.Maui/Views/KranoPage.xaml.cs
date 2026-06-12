using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class KranoPage : ContentPage
{
    public KranoPage(KranoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is KranoViewModel vm && vm.Preparing.Count == 0 && vm.Chests.Count == 0 && vm.Items.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
