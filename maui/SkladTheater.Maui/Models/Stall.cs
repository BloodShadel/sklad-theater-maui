using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class StallItem
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("added_at")] public string AddedAt { get; set; } = string.Empty;
}

public class StallChest
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("stall_id")] public int? StallId { get; set; }
    [JsonProperty("created_at")] public string CreatedAt { get; set; } = string.Empty;
    [JsonProperty("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
}

public class StallDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("street")] public string Street { get; set; } = string.Empty;
    [JsonProperty("stall_number")] public string StallNumber { get; set; } = string.Empty;
    [JsonProperty("shelf_number")] public string ShelfNumber { get; set; } = string.Empty;
    [JsonProperty("items")] public List<StallItem>? Items { get; set; }
    [JsonProperty("chests")] public List<StallChest>? Chests { get; set; }
    [JsonProperty("photo_url")] public string? PhotoUrl { get; set; }
    [JsonProperty("created_at")] public string? CreatedAt { get; set; }
    [JsonProperty("updated_at")] public string? UpdatedAt { get; set; }
    [JsonProperty("map_x")] public double? MapX { get; set; }
    [JsonProperty("map_y")] public double? MapY { get; set; }
    [JsonProperty("map_w")] public double? MapW { get; set; }
    [JsonProperty("map_h")] public double? MapH { get; set; }

    public string DisplayName => $"{Street}, стойло {StallNumber}, полка {ShelfNumber}";
}

public class AddStallRequest
{
    [JsonProperty("street")] public string Street { get; set; } = string.Empty;
    [JsonProperty("stallNumber")] public string StallNumber { get; set; } = string.Empty;
    [JsonProperty("shelfNumber")] public string ShelfNumber { get; set; } = string.Empty;
}

public class MapLabel
{
    [JsonProperty("id")] public string Id { get; set; } = string.Empty;
    [JsonProperty("text")] public string Text { get; set; } = string.Empty;
    [JsonProperty("map_x")] public double? MapX { get; set; }
    [JsonProperty("map_y")] public double? MapY { get; set; }
    [JsonProperty("map_w")] public double? MapW { get; set; }
    [JsonProperty("map_h")] public double? MapH { get; set; }
}

public class MapLabelsResponse
{
    [JsonProperty("labels")] public List<MapLabel> Labels { get; set; } = new();
}
