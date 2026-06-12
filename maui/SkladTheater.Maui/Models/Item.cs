using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class ItemStallDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("street")] public string Street { get; set; } = string.Empty;
    [JsonProperty("stall_number")] public string StallNumber { get; set; } = string.Empty;
    [JsonProperty("shelf_number")] public string ShelfNumber { get; set; } = string.Empty;
}

public class ItemChestDto
{
    [JsonProperty("chest_id")] public int ChestId { get; set; }
    [JsonProperty("chest_name")] public string? ChestName { get; set; }
    [JsonProperty("chest_location")] public string? ChestLocation { get; set; }
    [JsonProperty("quantity")] public int? Quantity { get; set; }
    [JsonProperty("stall_id")] public int? StallId { get; set; }
    [JsonProperty("stall_street")] public string? StallStreet { get; set; }
    [JsonProperty("stall_number")] public string? StallNumber { get; set; }
    [JsonProperty("stall_shelf")] public string? StallShelf { get; set; }
}

public class ItemKassetaDto
{
    [JsonProperty("kasseta_id")] public int KassetaId { get; set; }
    [JsonProperty("kasseta_number")] public string? KassetaNumber { get; set; }
    [JsonProperty("kasseta_shelf")] public string? KassetaShelf { get; set; }
    [JsonProperty("quantity")] public int? Quantity { get; set; }
}

public class ItemPhotoDto
{
    [JsonProperty("id")] public double Id { get; set; }
    [JsonProperty("url")] public string Url { get; set; } = string.Empty;
}

public class ItemDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("description")] public string? Description { get; set; }
    [JsonProperty("stall_id")] public int? StallId { get; set; }
    [JsonProperty("stall")] public ItemStallDto? Stall { get; set; }
    [JsonProperty("stalls")] public List<ItemStallDto>? Stalls { get; set; }
    [JsonProperty("chests")] public List<ItemChestDto>? Chests { get; set; }
    [JsonProperty("kassety")] public List<ItemKassetaDto>? Kassety { get; set; }
    [JsonProperty("shows")] public List<ShowDto>? Shows { get; set; }
    [JsonProperty("photos")] public List<ItemPhotoDto>? Photos { get; set; }
    [JsonProperty("photo_url")] public string? PhotoUrl { get; set; }
    [JsonProperty("location")] public string? Location { get; set; }
}

public class AddItemRequest
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
}

public class AddItemResponse
{
    [JsonProperty("success")] public bool Success { get; set; }
    [JsonProperty("id")] public int? Id { get; set; }
}

public class UpdateItemRequest
{
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("description")] public string? Description { get; set; }
    [JsonProperty("stallId")] public int? StallId { get; set; }
}

public class ApiResponse
{
    [JsonProperty("success")] public bool Success { get; set; }
    [JsonProperty("message")] public string? Message { get; set; }
}
