using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class TapePage : ContentPage
{
    public TapePage(TapeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TapeViewModel vm && vm.Tapes.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
