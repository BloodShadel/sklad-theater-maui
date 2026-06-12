namespace SkladTheater.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute("LoginPage", typeof(Views.LoginPage));
        Routing.RegisterRoute("MainPage", typeof(Views.ItemsPage));
        Routing.RegisterRoute("ProfilePage", typeof(Views.ProfilePage));
        Routing.RegisterRoute("ChatRoomPage", typeof(Views.ChatRoomPage));
        Routing.RegisterRoute("ItemsPage", typeof(Views.ItemsPage));
        Routing.RegisterRoute("TasksPage", typeof(Views.TasksPage));
        Routing.RegisterRoute("ChestsPage", typeof(Views.ChestsPage));
        Routing.RegisterRoute("StallsPage", typeof(Views.StallsPage));
        Routing.RegisterRoute("TapePage", typeof(Views.TapePage));
        Routing.RegisterRoute("ShowsPage", typeof(Views.ShowsPage));
        Routing.RegisterRoute("KassetyPage", typeof(Views.KassetyPage));
        Routing.RegisterRoute("PlaceholderPage", typeof(Views.PlaceholderPage));
    }
}
