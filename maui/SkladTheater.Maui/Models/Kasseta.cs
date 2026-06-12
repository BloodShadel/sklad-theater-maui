using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class KassetaDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("number")] public string? Number { get; set; }
    [JsonProperty("shelf")] public string? Shelf { get; set; }
}

public class KassetaItemDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("item_id")] public int ItemId { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("quantity")] public int? Quantity { get; set; }
}

public class AddKassetaRequest
{
    [JsonProperty("number")] public string Number { get; set; } = string.Empty;
    [JsonProperty("shelf")] public string Shelf { get; set; } = string.Empty;
}

public class AddKassetaResponse
{
    [JsonProperty("success")] public bool Success { get; set; }
    [JsonProperty("id")] public int? Id { get; set; }
}

public class AddKassetaItemRequest
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("quantity")] public int Quantity { get; set; }
}

public class UpdateKassetaItemRequest
{
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("quantity")] public int? Quantity { get; set; }
}
