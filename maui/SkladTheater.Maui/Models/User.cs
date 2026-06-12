using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class UserDto
{
    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("login")] public string Login { get; set; } = string.Empty;
    [JsonProperty("displayName")] public string DisplayName { get; set; } = string.Empty;
    [JsonProperty("role")] public string Role { get; set; } = string.Empty;
    [JsonProperty("roleName")] public string? RoleName { get; set; }
    [JsonProperty("avatarUrl")] public string? AvatarUrl { get; set; }
}

public class UserProfileDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("displayName")] public string DisplayName { get; set; } = string.Empty;
    [JsonProperty("login")] public string Login { get; set; } = string.Empty;
    [JsonProperty("avatarUrl")] public string? AvatarUrl { get; set; }
    [JsonProperty("online")] public bool Online { get; set; }
    [JsonProperty("lastOnline")] public string? LastOnline { get; set; }
}
