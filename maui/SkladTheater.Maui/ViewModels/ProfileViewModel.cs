using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkladTheater.Maui.Helpers;
using SkladTheater.Maui.Models;
using SkladTheater.Maui.Services;

namespace SkladTheater.Maui.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly ISkladApiService _api;

    [ObservableProperty]
    private UserDto? _user;

    [ObservableProperty]
    private string _avatarUrl = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    public ProfileViewModel(IAuthService authService, ISkladApiService api)
    {
        _authService = authService;
        _api = api;
    }

    public async Task LoadAsync()
    {
        IsBusy = true;
        try
        {
            var current = await _authService.GetCurrentUserAsync();
            User = current;
            AvatarUrl = ImageUrlHelper.Resolve(current?.AvatarUrl);
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
            AvatarUrl = ImageUrlHelper.Resolve(url);
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
