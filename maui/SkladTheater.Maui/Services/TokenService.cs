namespace SkladTheater.Maui.Services;

public interface ITokenService
{
    Task<string?> GetTokenAsync();
    Task SetTokenAsync(string token, bool persistent);
    Task<bool> IsPersistentAsync();
    Task<string?> GetSavedLoginAsync();
    Task SetSavedLoginAsync(string login);
    Task ClearAsync();
}

public class TokenService : ITokenService
{
    private const string TokenKey = "auth_token";
    private const string PersistentKey = "auth_persistent";
    private const string SavedLoginKey = "auth_saved_login";

    public Task<string?> GetTokenAsync()
        => SecureStorage.GetAsync(TokenKey);

    public async Task SetTokenAsync(string token, bool persistent)
    {
        await SecureStorage.SetAsync(TokenKey, token);
        await SecureStorage.SetAsync(PersistentKey, persistent.ToString());
    }

    public async Task<bool> IsPersistentAsync()
    {
        var value = await SecureStorage.GetAsync(PersistentKey);
        return bool.TryParse(value, out var result) && result;
    }

    public Task<string?> GetSavedLoginAsync()
        => SecureStorage.GetAsync(SavedLoginKey);

    public Task SetSavedLoginAsync(string login)
        => SecureStorage.SetAsync(SavedLoginKey, login);

    public Task ClearAsync()
    {
        SecureStorage.Remove(TokenKey);
        SecureStorage.Remove(PersistentKey);
        SecureStorage.Remove(SavedLoginKey);
        return Task.CompletedTask;
    }
}
