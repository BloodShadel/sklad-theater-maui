namespace SkladTheater.Maui.Services;

public interface IPreferenceService
{
    string? Get(string key);
    void Set(string key, string? value);
    void Remove(string key);
    bool Contains(string key);
    T? GetObject<T>(string key);
    void SetObject<T>(string key, T value);
}

public class PreferenceService : IPreferenceService
{
    public string? Get(string key) => Preferences.Get(key, default(string?));
    public void Set(string key, string? value) => Preferences.Set(key, value);
    public void Remove(string key) => Preferences.Remove(key);
    public bool Contains(string key) => Preferences.ContainsKey(key);

    public T? GetObject<T>(string key)
    {
        var json = Get(key);
        if (string.IsNullOrWhiteSpace(json)) return default;
        try
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        catch
        {
            return default;
        }
    }

    public void SetObject<T>(string key, T value)
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        Set(key, json);
    }
}
