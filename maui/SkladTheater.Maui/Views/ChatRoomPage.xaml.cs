using SkladTheater.Maui.ViewModels;

namespace SkladTheater.Maui.Views;

public partial class ChatRoomPage : ContentPage
{
    public ChatRoomPage(ChatRoomViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
