using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class TapeDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("quantity")] public int Quantity { get; set; }
    [JsonProperty("seasonReceived")] public int SeasonReceived { get; set; }
    [JsonProperty("seasonSpent")] public int SeasonSpent { get; set; }
}

public class SeasonStats
{
    [JsonProperty("totalReceived")] public int TotalReceived { get; set; }
    [JsonProperty("totalSpent")] public int TotalSpent { get; set; }
}

public class TapesListResponse
{
    [JsonProperty("tapes")] public List<TapeDto> Tapes { get; set; } = new();
    [JsonProperty("seasonStats")] public SeasonStats SeasonStats { get; set; } = new();
}

public class TapeAddRequest
{
    [JsonProperty("tapeId")] public int TapeId { get; set; }
    [JsonProperty("amount")] public int Amount { get; set; }
    [JsonProperty("reason")] public string? Reason { get; set; }
}

public class TapeUseRequest : TapeAddRequest { }
public class TapeReturnRequest : TapeAddRequest { }

public class TapeUpdateRequest
{
    [JsonProperty("tapeId")] public int TapeId { get; set; }
    [JsonProperty("quantity")] public int Quantity { get; set; }
    [JsonProperty("reason")] public string? Reason { get; set; }
}

public class TapeHistoryItem
{
    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("tapeId")] public int TapeId { get; set; }
    [JsonProperty("tapeName")] public string TapeName { get; set; } = string.Empty;
    [JsonProperty("oldQuantity")] public int OldQuantity { get; set; }
    [JsonProperty("newQuantity")] public int NewQuantity { get; set; }
    [JsonProperty("change")] public int Change { get; set; }
    [JsonProperty("reason")] public string? Reason { get; set; }
    [JsonProperty("userId")] public int UserId { get; set; }
    [JsonProperty("userName")] public string UserName { get; set; } = string.Empty;
    [JsonProperty("timestamp")] public string Timestamp { get; set; } = string.Empty;
}
