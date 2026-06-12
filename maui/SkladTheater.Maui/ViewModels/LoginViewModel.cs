using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;

namespace SkladTheater.Maui.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IWebSocketService _webSocketService;

    [ObservableProperty]
    private string _login = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private bool _rememberMe = true;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    partial void OnLoginChanged(string value) => ErrorMessage = string.Empty;
    partial void OnPasswordChanged(string value) => ErrorMessage = string.Empty;

    public LoginViewModel(IAuthService authService, ITokenService tokenService, IWebSocketService webSocketService)
    {
        _authService = authService;
        _tokenService = tokenService;
        _webSocketService = webSocketService;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Login))
        {
            ErrorMessage = "Введите логин";
            return;
        }
        if (string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Введите пароль";
            return;
        }

        IsBusy = true;
        ErrorMessage = string.Empty;

        try
        {
            var user = await _authService.LoginAsync(Login.Trim(), Password, RememberMe);
            if (user != null)
            {
                await _webSocketService.ConnectAsync((int)user.Id);
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                ErrorMessage = "Не удалось войти";
            }
        }
        catch (AuthException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
