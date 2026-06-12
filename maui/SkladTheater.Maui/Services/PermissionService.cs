using SkladTheater.Maui.Models;

namespace SkladTheater.Maui.Services;

public interface IPermissionService
{
    Task FetchAndStoreAsync();
    bool HasPermission(string href);
    bool IsAdmin();
    bool IsManagerOrAdmin();
    string? Role { get; }
    void Clear();
}

public class PermissionService : IPermissionService
{
    private const string RoleKey = "permissions_role";
    private const string PermissionsKey = "permissions_list";
    private const string IsAdminKey = "permissions_is_admin";

    private readonly IApiClient _api;
    private readonly IPreferenceService _prefs;

    private string? _role;
    private List<string>? _permissions;
    private bool _isAdmin;

    public string? Role => _role;

    public PermissionService(IApiClient api, IPreferenceService prefs)
    {
        _api = api;
        _prefs = prefs;
        LoadFromCache();
    }

    public async Task FetchAndStoreAsync()
    {
        try
        {
            var response = await _api.GetAsync<MyPermissionsResponse>("api/admin/my-permissions");
            if (response == null) return;

            _role = response.Role;
            _permissions = response.Permissions;
            _isAdmin = response.Permissions == null;

            _prefs.Set(RoleKey, _role);
            _prefs.Set(IsAdminKey, _isAdmin.ToString());
            _prefs.SetObject(PermissionsKey, _permissions ?? new List<string>());
        }
        catch
        {
            // если не удалось загрузить — оставляем кэш
        }
    }

    public bool HasPermission(string href)
    {
        if (_isAdmin) return true;
        return _permissions?.Contains(href, StringComparer.OrdinalIgnoreCase) ?? false;
    }

    public bool IsAdmin() => _isAdmin;

    public bool IsManagerOrAdmin() => _isAdmin ||
        string.Equals(_role, "admin", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(_role, "nasyalnika", StringComparison.OrdinalIgnoreCase);

    public void Clear()
    {
        _role = null;
        _permissions = null;
        _isAdmin = false;
        _prefs.Remove(RoleKey);
        _prefs.Remove(PermissionsKey);
        _prefs.Remove(IsAdminKey);
    }

    private void LoadFromCache()
    {
        _role = _prefs.Get(RoleKey);
        var isAdminStr = _prefs.Get(IsAdminKey);
        _isAdmin = bool.TryParse(isAdminStr, out var parsed) && parsed;
        _permissions = _prefs.GetObject<List<string>>(PermissionsKey);
    }
}
