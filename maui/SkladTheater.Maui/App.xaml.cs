using SkladTheater.Maui.Services;

namespace SkladTheater.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override async void OnStart()
    {
        base.OnStart();

        var services = Handler?.MauiContext?.Services;
        if (services == null)
        {
            MainPage = new AppShell();
            return;
        }

        var tokenService = services.GetRequiredService<ITokenService>();
        var authService = services.GetRequiredService<IAuthService>();
        var webSocketService = services.GetRequiredService<IWebSocketService>();

        var token = await tokenService.GetTokenAsync();
        var persistent = await tokenService.IsPersistentAsync();

        var shell = new AppShell();
        MainPage = shell;

        if (!string.IsNullOrEmpty(token) && persistent)
        {
            try
            {
                var user = await authService.GetCurrentUserAsync();
                if (user != null)
                {
                    await webSocketService.ConnectAsync((int)user.Id);
                    await shell.GoToAsync("//MainPage");
                    return;
                }
            }
            catch
            {
                // ignore, stay on login
            }
        }

        await shell.GoToAsync("//LoginPage");
    }
}
