using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class ChatsPage : ContentPage
{
    public ChatsPage(ChatsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ChatsViewModel vm)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
