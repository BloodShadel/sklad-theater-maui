using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class HealthResponse
{
    [JsonProperty("status")] public string Status { get; set; } = string.Empty;
    [JsonProperty("message")] public string? Message { get; set; }
    [JsonProperty("version")] public string? Version { get; set; }
}

public class VersionResponse
{
    [JsonProperty("latestVersion")] public string LatestVersion { get; set; } = string.Empty;
    [JsonProperty("currentVersion")] public string CurrentVersion { get; set; } = string.Empty;
    [JsonProperty("needsUpdate")] public bool NeedsUpdate { get; set; }
    [JsonProperty("downloadUrl")] public string? DownloadUrl { get; set; }
    [JsonProperty("message")] public string Message { get; set; } = string.Empty;
}
