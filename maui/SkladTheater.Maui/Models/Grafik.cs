using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class ScheduleCell
{
    [JsonProperty("text")] public string? Text { get; set; }
    [JsonProperty("color")] public string? Color { get; set; }
}

public class ScheduleMonth
{
    [JsonProperty("plans")] public Dictionary<string, string> Plans { get; set; } = new();
    [JsonProperty("cells")] public Dictionary<string, Dictionary<string, ScheduleCell>> Cells { get; set; } = new();
    [JsonProperty("rowColors")] public Dictionary<string, string> RowColors { get; set; } = new();
    [JsonProperty("colColors")] public Dictionary<string, string> ColColors { get; set; } = new();
    [JsonProperty("shifts")] public Dictionary<string, int> Shifts { get; set; } = new();
}

public class ScheduleResponse
{
    [JsonProperty("month")] public string Month { get; set; } = string.Empty;
    [JsonProperty("schedule")] public ScheduleMonth Schedule { get; set; } = new();
}

public class ScheduleSaveRequest
{
    [JsonProperty("schedule")] public ScheduleMonth Schedule { get; set; } = new();
}

public class ScheduleSaveResponse
{
    [JsonProperty("success")] public bool Success { get; set; }
    [JsonProperty("month")] public string Month { get; set; } = string.Empty;
}
