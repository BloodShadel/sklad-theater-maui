using Microsoft.Extensions.Logging;
using SkladTheater.Maui.Services;
using SkladTheater.Maui.ViewModels;
using SkladTheater.Maui.Views;

namespace SkladTheater.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services
        builder.Services.AddSingleton<IPreferenceService, PreferenceService>();
        builder.Services.AddSingleton<ITokenService, TokenService>();
        builder.Services.AddSingleton<IApiClient, ApiClient>();
        builder.Services.AddSingleton<IPermissionService, PermissionService>();
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<ISkladApiService, SkladApiService>();
        builder.Services.AddSingleton<IOfflineCacheService, OfflineCacheService>();
        builder.Services.AddSingleton<IWebSocketService, WebSocketService>();

        // ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<ItemsViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<MoreViewModel>();

        // Views
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<ItemsPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<MorePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
