using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class Chest
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("location")] public string? Location { get; set; }
    [JsonProperty("stall_id")] public int? StallId { get; set; }
    [JsonProperty("stall_street")] public string? StallStreet { get; set; }
    [JsonProperty("stall_number")] public string? StallNumber { get; set; }
    [JsonProperty("stall_shelf")] public string? StallShelf { get; set; }
    [JsonProperty("created_at")] public string CreatedAt { get; set; } = string.Empty;
    [JsonProperty("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
}

public class ChestWithStall
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("stall_id")] public int? StallId { get; set; }
    [JsonProperty("stall_number")] public string? StallNumber { get; set; }
    [JsonProperty("shelf_number")] public string? ShelfNumber { get; set; }
    [JsonProperty("street")] public string? Street { get; set; }
}

public class AddChestRequest
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("stallId")] public int? StallId { get; set; }
}

public class AddChestResponse
{
    [JsonProperty("success")] public bool Success { get; set; }
    [JsonProperty("id")] public int? Id { get; set; }
}

public class UpdateChestRequest
{
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("stallId")] public int? StallId { get; set; }
}

public class UpdateChestResponse
{
    [JsonProperty("success")] public bool Success { get; set; }
    [JsonProperty("chest")] public Chest? Chest { get; set; }
}

public class DeleteChestResponse
{
    [JsonProperty("success")] public bool Success { get; set; }
}

public class GenericResponse
{
    [JsonProperty("success")] public bool Success { get; set; }
}

public class LocationRequest
{
    [JsonProperty("location")] public string Location { get; set; } = string.Empty;
}
