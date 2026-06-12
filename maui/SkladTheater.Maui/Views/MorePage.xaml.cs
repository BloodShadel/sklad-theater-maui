using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class MorePage : ContentPage
{
    public MorePage(MoreViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MoreViewModel vm)
            vm.LoadCommand.Execute(null);
    }
}
