using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class KassetyPage : ContentPage
{
    public KassetyPage(KassetyViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is KassetyViewModel vm && vm.Kassety.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
