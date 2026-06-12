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
        Routing.RegisterRoute("MainPage", typeof(Views.ItemsPage)); // placeholder
        Routing.RegisterRoute("ProfilePage", typeof(Views.ProfilePage));
    }
}
