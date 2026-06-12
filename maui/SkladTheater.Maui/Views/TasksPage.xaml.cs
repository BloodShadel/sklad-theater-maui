using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class TasksPage : ContentPage
{
    public TasksPage(TasksViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TasksViewModel vm && vm.Tasks.Count == 0)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
