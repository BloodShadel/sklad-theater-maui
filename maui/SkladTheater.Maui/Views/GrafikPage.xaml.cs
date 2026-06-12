using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class GrafikPage : ContentPage
{
    public GrafikPage(GrafikViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is GrafikViewModel vm && vm.Rows.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
