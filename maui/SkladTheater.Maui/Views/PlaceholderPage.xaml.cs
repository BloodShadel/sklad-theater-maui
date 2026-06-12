using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class PlaceholderPage : ContentPage
{
    public PlaceholderPage(PlaceholderViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
