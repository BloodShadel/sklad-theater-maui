using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class ItemsPage : ContentPage
{
    public ItemsPage(ItemsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ItemsViewModel vm && vm.Items.Count == 0)
        {
            await vm.LoadCommand.ExecuteAsync(null);
        }
    }
}
