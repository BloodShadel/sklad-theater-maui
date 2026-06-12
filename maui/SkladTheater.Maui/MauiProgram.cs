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
        builder.Services.AddTransient<TasksViewModel>();
        builder.Services.AddTransient<ChestsViewModel>();
        builder.Services.AddTransient<StallsViewModel>();
        builder.Services.AddTransient<TapeViewModel>();
        builder.Services.AddTransient<ShowsViewModel>();
        builder.Services.AddTransient<KassetyViewModel>();
        builder.Services.AddTransient<GrafikViewModel>();
        builder.Services.AddTransient<KranoViewModel>();
        builder.Services.AddTransient<HistoryViewModel>();
        builder.Services.AddTransient<RolesViewModel>();
        builder.Services.AddTransient<UsersViewModel>();
        builder.Services.AddTransient<ChatsViewModel>();
        builder.Services.AddTransient<ChatRoomViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<MoreViewModel>();
        builder.Services.AddTransient<PlaceholderViewModel>();

        // Views
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<ItemsPage>();
        builder.Services.AddTransient<TasksPage>();
        builder.Services.AddTransient<ChestsPage>();
        builder.Services.AddTransient<StallsPage>();
        builder.Services.AddTransient<TapePage>();
        builder.Services.AddTransient<ShowsPage>();
        builder.Services.AddTransient<KassetyPage>();
        builder.Services.AddTransient<GrafikPage>();
        builder.Services.AddTransient<KranoPage>();
        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<RolesPage>();
        builder.Services.AddTransient<UsersPage>();
        builder.Services.AddTransient<ChatsPage>();
        builder.Services.AddTransient<ChatRoomPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<MorePage>();
        builder.Services.AddTransient<PlaceholderPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
