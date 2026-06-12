using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class MyPermissionsResponse
{
    [JsonProperty("role")] public string Role { get; set; } = string.Empty;
    [JsonProperty("permissions")] public List<string>? Permissions { get; set; }
}

public class RoleDto
{
    [JsonProperty("id")] public string Id { get; set; } = string.Empty;
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("permissions")] public List<string>? Permissions { get; set; }
}

public class RolesListResponse
{
    [JsonProperty("roles")] public List<RoleDto> Roles { get; set; } = new();
}

public class UsersListResponse
{
    [JsonProperty("users")] public List<UserDto> Users { get; set; } = new();
}

public class CreateRoleRequest
{
    [JsonProperty("id")] public string Id { get; set; } = string.Empty;
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
}

public class UpdateRoleRequest
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
}

public class UpdatePermissionsRequest
{
    [JsonProperty("permissions")] public List<string>? Permissions { get; set; }
}
