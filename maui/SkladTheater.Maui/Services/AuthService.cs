using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkladTheater.Maui.Models;

namespace SkladTheater.Maui.Services;

public interface IAuthService
{
    Task<UserDto?> LoginAsync(string login, string password, bool rememberMe);
    Task<UserDto?> GetCurrentUserAsync();
    Task<string?> UploadAvatarAsync(Stream stream, string fileName);
    Task LogoutAsync();
}

public class AuthService : IAuthService
{
    private readonly IApiClient _api;
    private readonly ITokenService _tokenService;
    private readonly IPermissionService _permissionService;

    public AuthService(IApiClient api, ITokenService tokenService, IPermissionService permissionService)
    {
        _api = api;
        _tokenService = tokenService;
        _permissionService = permissionService;
    }

    public async Task<UserDto?> LoginAsync(string login, string password, bool rememberMe)
    {
        try
        {
            var response = await _api.PostAsync<AuthResponse>("api/auth/login", new LoginRequest
            {
                Login = login,
                Password = password
            });

            if (response == null || string.IsNullOrEmpty(response.Token)) return null;

            await _tokenService.SetTokenAsync(response.Token, rememberMe);
            if (rememberMe)
            {
                await _tokenService.SetSavedLoginAsync(login);
            }

            await _permissionService.FetchAndStoreAsync();
            return response.User;
        }
        catch (ApiException ex) when (ex.StatusCode == 401 || ex.StatusCode == 403)
        {
            var message = ParseErrorMessage(ex.ResponseContent);
            throw new AuthException(message, ex);
        }
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        var token = await _tokenService.GetTokenAsync();
        if (string.IsNullOrEmpty(token)) return null;

        try
        {
            var response = await _api.GetAsync<MeResponse>("api/auth/me");
            return response?.User;
        }
        catch (ApiException ex) when (ex.StatusCode == 401)
        {
            await _tokenService.ClearAsync();
            return null;
        }
    }

    public async Task<string?> UploadAvatarAsync(Stream stream, string fileName)
    {
        var content = new StreamContent(stream);
        var response = await _api.PostMultipartAsync<AvatarResponse>("api/auth/avatar", content, "avatar", fileName);
        return response?.AvatarUrl;
    }

    public async Task LogoutAsync()
    {
        await _tokenService.ClearAsync();
        _permissionService.Clear();
    }

    private static string ParseErrorMessage(string json)
    {
        try
        {
            var obj = JObject.Parse(json);
            return obj["error"]?.ToString() ?? "Ошибка авторизации";
        }
        catch
        {
            return "Ошибка авторизации";
        }
    }
}

public class AuthException : Exception
{
    public AuthException(string message, Exception? inner = null) : base(message, inner) { }
}
