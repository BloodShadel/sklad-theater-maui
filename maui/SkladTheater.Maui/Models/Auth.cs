using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class LoginRequest
{
    [JsonProperty("login")] public string Login { get; set; } = string.Empty;
    [JsonProperty("password")] public string Password { get; set; } = string.Empty;
}

public class RegisterRequest
{
    [JsonProperty("login")] public string Login { get; set; } = string.Empty;
    [JsonProperty("password")] public string Password { get; set; } = string.Empty;
    [JsonProperty("displayName")] public string? DisplayName { get; set; }
}

public class AuthResponse
{
    [JsonProperty("user")] public UserDto User { get; set; } = new();
    [JsonProperty("token")] public string Token { get; set; } = string.Empty;
}

public class MeResponse
{
    [JsonProperty("user")] public UserDto User { get; set; } = new();
}

public class AvatarResponse
{
    [JsonProperty("avatarUrl")] public string AvatarUrl { get; set; } = string.Empty;
}
