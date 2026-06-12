using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Helpers;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;

namespace SkladTheater.Maui.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly ISkladApiService _api;

    [ObservableProperty]
    private UserDto? _user;

    [ObservableProperty]
    private string _avatarUrl = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    public ProfileViewModel(IAuthService authService, ITokenService tokenService, ISkladApiService api)
    {
        _authService = authService;
        _tokenService = tokenService;
        _api = api;
    }

    public async Task LoadAsync()
    {
        IsBusy = true;
        try
        {
            var current = await _authService.GetCurrentUserAsync();
            User = current;
            if (!string.IsNullOrEmpty(current?.AvatarUrl))
            {
                AvatarUrl = current.AvatarUrl.StartsWith("http")
                    ? current.AvatarUrl
                    : AppConfig.BaseUrl.TrimEnd('/') + current.AvatarUrl;
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task PickAvatarAsync()
    {
        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions { Title = "Выберите аватар" });
            if (photo == null) return;

            IsBusy = true;
            await using var stream = await photo.OpenReadAsync();
            var fileName = photo.FileName ?? "avatar.jpg";
            var url = await _authService.UploadAvatarAsync(stream, fileName);
            if (!string.IsNullOrEmpty(url))
            {
                AvatarUrl = url.StartsWith("http") ? url : AppConfig.BaseUrl.TrimEnd('/') + url;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
