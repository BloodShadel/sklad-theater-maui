using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class ShowDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("created_at")] public string? CreatedAt { get; set; }
    [JsonProperty("updated_at")] public string? UpdatedAt { get; set; }
}

public class ShowItemDto
{
    [JsonProperty("item_id")] public int ItemId { get; set; }
    [JsonProperty("item_name")] public string ItemName { get; set; } = string.Empty;
    [JsonProperty("stall_id")] public int StallId { get; set; }
    [JsonProperty("stall_name")] public string StallName { get; set; } = string.Empty;
}

public class AddShowRequest
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
}

public class UpdateShowRequest
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
}
