using SkladTheater.Maui.Models;

namespace SkladTheater.Maui.Services;

public interface ISkladApiService
{
    // Health / Version
    Task<HealthResponse?> GetHealthAsync(CancellationToken ct = default);
    Task<VersionResponse?> GetVersionAsync(string currentVersion, CancellationToken ct = default);

    // Admin / Users
    Task<List<UserDto>> GetUsersAsync(CancellationToken ct = default);
    Task<List<RoleDto>> GetRolesAsync(CancellationToken ct = default);
    Task CreateRoleAsync(string id, string name, CancellationToken ct = default);
    Task UpdateRoleAsync(string id, string name, CancellationToken ct = default);
    Task DeleteRoleAsync(string id, CancellationToken ct = default);
    Task UpdateRolePermissionsAsync(string id, List<string>? permissions, CancellationToken ct = default);

    // Stalls
    Task<List<StallDto>> GetStallsAsync(string? search = null, CancellationToken ct = default);
    Task<MapLabelsResponse?> GetMapLabelsAsync(CancellationToken ct = default);
    Task<StallDto?> GetStallAsync(int id, CancellationToken ct = default);
    Task<ApiResponse?> AddStallAsync(string street, string stallNumber, string shelfNumber, CancellationToken ct = default);
    Task<ApiResponse?> AddStallItemAsync(int stallId, string name, CancellationToken ct = default);
    Task<ApiResponse?> DeleteStallItemAsync(int stallId, int itemId, CancellationToken ct = default);
    Task<ApiResponse?> DeleteStallAsync(int id, CancellationToken ct = default);

    // Chests
    Task<List<Chest>> GetChestsAsync(string? search = null, CancellationToken ct = default);
    Task<List<Chest>> GetChestsByLocationAsync(string location, CancellationToken ct = default);
    Task<AddChestResponse?> AddChestAsync(string name, int? stallId, CancellationToken ct = default);
    Task<UpdateChestResponse?> UpdateChestAsync(int id, string? name, int? stallId, CancellationToken ct = default);
    Task<ApiResponse?> UpdateChestLocationAsync(int id, string location, CancellationToken ct = default);
    Task<DeleteChestResponse?> DeleteChestAsync(int id, CancellationToken ct = default);
    Task<List<ShowDto>> GetChestShowsAsync(int id, CancellationToken ct = default);
    Task<GenericResponse?> AddChestShowAsync(int chestId, int showId, CancellationToken ct = default);
    Task<GenericResponse?> DeleteChestShowAsync(int chestId, int showId, CancellationToken ct = default);

    // Items
    Task<List<ItemDto>> GetItemsAsync(CancellationToken ct = default);
    Task<ItemDto?> GetItemAsync(int id, CancellationToken ct = default);
    Task<AddItemResponse?> AddItemAsync(string name, CancellationToken ct = default);
    Task<ApiResponse?> UpdateItemAsync(int id, UpdateItemRequest request, CancellationToken ct = default);
    Task<ApiResponse?> DeleteItemAsync(int id, CancellationToken ct = default);
    Task<ApiResponse?> UpdateItemLocationAsync(int id, string location, CancellationToken ct = default);
    Task<List<ShowDto>> GetItemShowsAsync(int id, CancellationToken ct = default);
    Task<ApiResponse?> AddItemShowAsync(int itemId, int showId, CancellationToken ct = default);
    Task<ApiResponse?> DeleteItemShowAsync(int itemId, int showId, CancellationToken ct = default);
    Task<List<ItemStallDto>> GetItemStallsAsync(int id, CancellationToken ct = default);
    Task<ApiResponse?> AddItemStallAsync(int itemId, int stallId, CancellationToken ct = default);
    Task<ApiResponse?> DeleteItemStallAsync(int itemId, int stallId, CancellationToken ct = default);
    Task<ApiResponse?> AddItemChestAsync(int itemId, int chestId, CancellationToken ct = default);
    Task<ApiResponse?> DeleteItemChestAsync(int itemId, int chestId, CancellationToken ct = default);
    Task<ApiResponse?> AddItemKassetaAsync(int itemId, int kassetaId, CancellationToken ct = default);
    Task<ApiResponse?> DeleteItemKassetaAsync(int itemId, int kassetaId, CancellationToken ct = default);
    Task<List<ItemPhotoDto>> GetItemPhotosAsync(int id, CancellationToken ct = default);
    Task<ApiResponse?> UploadItemPhotoAsync(int id, Stream stream, string fileName, CancellationToken ct = default);
    Task<ApiResponse?> DeleteItemPhotoAsync(int itemId, double photoId, CancellationToken ct = default);

    // Shows
    Task<List<ShowDto>> GetShowsAsync(CancellationToken ct = default);
    Task<ShowDto?> GetShowAsync(int id, CancellationToken ct = default);
    Task<List<ShowItemDto>> GetShowItemsAsync(int id, CancellationToken ct = default);
    Task<ApiResponse?> AddShowAsync(string name, CancellationToken ct = default);
    Task<ApiResponse?> UpdateShowAsync(int id, string name, CancellationToken ct = default);
    Task<ApiResponse?> DeleteShowAsync(int id, CancellationToken ct = default);

    // Tasks
    Task<List<TaskDto>> GetTasksAsync(CancellationToken ct = default);
    Task<List<int>> GetUserShowsAsync(int userId, CancellationToken ct = default);
    Task<NewTasksResponse?> GetNewTasksAsync(int userId, DateTime? since, CancellationToken ct = default);
    Task<TaskDto?> GetTaskAsync(int id, CancellationToken ct = default);
    Task<ApiResponse?> UpdateTaskStatusAsync(int id, string status, string? comment, CancellationToken ct = default);
    Task<ApiResponse?> UpdateTaskAsync(int id, TaskUpdateRequest request, CancellationToken ct = default);
    Task<ApiResponse?> AddTaskAsync(TaskAddRequest request, CancellationToken ct = default);
    Task<ApiResponse?> DeleteTaskAsync(int id, CancellationToken ct = default);
    Task<List<TaskHistoryItem>> GetTaskHistoryAsync(int limit = 100, CancellationToken ct = default);
    Task<List<TaskHistoryItem>> GetCompletedTaskHistoryAsync(int limit = 100, CancellationToken ct = default);

    // Tape
    Task<TapesListResponse?> GetTapesAsync(CancellationToken ct = default);
    Task<ApiResponse?> AddTapeAsync(int tapeId, int amount, string? reason, CancellationToken ct = default);
    Task<ApiResponse?> UseTapeAsync(int tapeId, int amount, string? reason, CancellationToken ct = default);
    Task<ApiResponse?> ReturnTapeAsync(int tapeId, int amount, string? reason, CancellationToken ct = default);
    Task<ApiResponse?> UpdateTapeAsync(int tapeId, int quantity, string? reason, CancellationToken ct = default);
    Task<List<TapeHistoryItem>> GetTapeHistoryAsync(CancellationToken ct = default);

    // Kassety
    Task<List<KassetaDto>> GetKassetyAsync(CancellationToken ct = default);
    Task<AddKassetaResponse?> AddKassetaAsync(string number, string shelf, CancellationToken ct = default);
    Task<ApiResponse?> UpdateKassetaAsync(int id, string number, string shelf, CancellationToken ct = default);
    Task<ApiResponse?> DeleteKassetaAsync(int id, CancellationToken ct = default);
    Task<List<KassetaItemDto>> GetKassetaItemsAsync(int id, CancellationToken ct = default);
    Task<ApiResponse?> AddKassetaItemAsync(int id, string name, int quantity, CancellationToken ct = default);
    Task<ApiResponse?> UpdateKassetaItemAsync(int id, int itemId, string? name, int? quantity, CancellationToken ct = default);
    Task<ApiResponse?> DeleteKassetaItemAsync(int id, int itemId, CancellationToken ct = default);

    // Grafik
    Task<ScheduleResponse?> GetScheduleAsync(string month, CancellationToken ct = default);
    Task<ScheduleSaveResponse?> SaveScheduleAsync(string month, ScheduleMonth schedule, CancellationToken ct = default);

    // Chat
    Task<List<ChatUserDto>> GetChatUsersAsync(CancellationToken ct = default);
    Task<List<ChatDto>> GetChatsAsync(CancellationToken ct = default);
    Task<CreateChatResponse?> CreateDirectChatAsync(int participantId, CancellationToken ct = default);
    Task<CreateChatResponse?> CreateGroupChatAsync(string name, List<int> participantIds, CancellationToken ct = default);
    Task<List<ChatMessageDto>> GetMessagesAsync(int chatId, int limit = 50, int? before = null, CancellationToken ct = default);
    Task<ChatMessageDto?> SendMessageAsync(int chatId, string text, int? replyToId, CancellationToken ct = default);
    Task<ChatMessageDto?> EditMessageAsync(int chatId, int msgId, string text, CancellationToken ct = default);
    Task DeleteMessageAsync(int chatId, int msgId, CancellationToken ct = default);
    Task<ChatMessageDto?> ForwardMessageAsync(int chatId, int originalMsgId, CancellationToken ct = default);
    Task<ChatMessageDto?> UploadChatPhotoAsync(int chatId, Stream stream, string fileName, CancellationToken ct = default);
    Task<ChatMessageDto?> UploadChatVoiceAsync(int chatId, Stream stream, string fileName, int duration, CancellationToken ct = default);
    Task<ChatMessageDto?> ReactAsync(int chatId, int msgId, string emoji, CancellationToken ct = default);
    Task PinMessageAsync(int chatId, int? messageId, CancellationToken ct = default);
    Task<ChatInfoDto?> GetChatInfoAsync(int chatId, CancellationToken ct = default);
    Task LeaveChatAsync(int chatId, CancellationToken ct = default);
    Task RenameChatAsync(int chatId, string name, CancellationToken ct = default);
    Task<UserProfileDto?> GetUserProfileAsync(int id, CancellationToken ct = default);
    Task AddChatMemberAsync(int chatId, int userId, CancellationToken ct = default);
    Task RemoveChatMemberAsync(int chatId, int memberId, CancellationToken ct = default);
    Task MarkReadAsync(int chatId, List<int> messageIds, CancellationToken ct = default);
    Task<List<UnreadChatSummaryDto>> GetUnreadSummaryAsync(DateTime? since, CancellationToken ct = default);
}

public class SkladApiService : ISkladApiService
{
    private readonly IApiClient _api;

    public SkladApiService(IApiClient api)
    {
        _api = api;
    }

    #region Health / Version
    public Task<HealthResponse?> GetHealthAsync(CancellationToken ct = default)
        => _api.GetAsync<HealthResponse>("api/health", ct);

    public Task<VersionResponse?> GetVersionAsync(string currentVersion, CancellationToken ct = default)
        => _api.GetAsync<VersionResponse>($"api/version?version={Uri.EscapeDataString(currentVersion)}", ct);
    #endregion

    #region Admin / Users
    public async Task<List<UserDto>> GetUsersAsync(CancellationToken ct = default)
    {
        var response = await _api.GetAsync<UsersListResponse>("api/admin/users", ct);
        return response?.Users ?? new List<UserDto>();
    }

    public async Task<List<RoleDto>> GetRolesAsync(CancellationToken ct = default)
    {
        var response = await _api.GetAsync<RolesListResponse>("api/admin/roles", ct);
        return response?.Roles ?? new List<RoleDto>();
    }

    public Task CreateRoleAsync(string id, string name, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>("api/admin/roles", new CreateRoleRequest { Id = id, Name = name }, ct);

    public Task UpdateRoleAsync(string id, string name, CancellationToken ct = default)
        => _api.PutAsync<ApiResponse>($"api/admin/roles/{id}", new UpdateRoleRequest { Name = name }, ct);

    public Task DeleteRoleAsync(string id, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/admin/roles/{id}", ct);

    public Task UpdateRolePermissionsAsync(string id, List<string>? permissions, CancellationToken ct = default)
        => _api.PutAsync<ApiResponse>($"api/admin/roles/{id}/permissions", new UpdatePermissionsRequest { Permissions = permissions }, ct);
    #endregion

    #region Stalls
    public async Task<List<StallDto>> GetStallsAsync(string? search = null, CancellationToken ct = default)
    {
        var url = "api/stalls/list";
        if (!string.IsNullOrWhiteSpace(search))
            url += $"?search={Uri.EscapeDataString(search)}";
        var result = await _api.GetAsync<List<StallDto>>(url, ct);
        return result ?? new List<StallDto>();
    }

    public Task<MapLabelsResponse?> GetMapLabelsAsync(CancellationToken ct = default)
        => _api.GetAsync<MapLabelsResponse>("api/stalls/labels", ct);

    public Task<StallDto?> GetStallAsync(int id, CancellationToken ct = default)
        => _api.GetAsync<StallDto>($"api/stalls/{id}", ct);

    public Task<ApiResponse?> AddStallAsync(string street, string stallNumber, string shelfNumber, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>("api/stalls/add", new AddStallRequest { Street = street, StallNumber = stallNumber, ShelfNumber = shelfNumber }, ct);

    public Task<ApiResponse?> AddStallItemAsync(int stallId, string name, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>($"api/stalls/{stallId}/items", new AddItemRequest { Name = name }, ct);

    public Task<ApiResponse?> DeleteStallItemAsync(int stallId, int itemId, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/stalls/{stallId}/items/{itemId}", ct);

    public Task<ApiResponse?> DeleteStallAsync(int id, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/stalls/{id}", ct);
    #endregion

    #region Chests
    public async Task<List<Chest>> GetChestsAsync(string? search = null, CancellationToken ct = default)
    {
        var url = "/api/chests/list";
        if (!string.IsNullOrWhiteSpace(search))
            url += $"?search={Uri.EscapeDataString(search)}";
        var result = await _api.GetAsync<List<Chest>>(url, ct);
        return result ?? new List<Chest>();
    }

    public async Task<List<Chest>> GetChestsByLocationAsync(string location, CancellationToken ct = default)
    {
        var result = await _api.GetAsync<List<Chest>>($"/api/chests/location/{location}", ct);
        return result ?? new List<Chest>();
    }

    public Task<AddChestResponse?> AddChestAsync(string name, int? stallId, CancellationToken ct = default)
        => _api.PostAsync<AddChestResponse>("/api/chests/add", new AddChestRequest { Name = name, StallId = stallId }, ct);

    public Task<UpdateChestResponse?> UpdateChestAsync(int id, string? name, int? stallId, CancellationToken ct = default)
        => _api.PutAsync<UpdateChestResponse>($"/api/chests/{id}", new UpdateChestRequest { Name = name, StallId = stallId }, ct);

    public Task<ApiResponse?> UpdateChestLocationAsync(int id, string location, CancellationToken ct = default)
        => _api.PutAsync<ApiResponse>($"/api/chests/{id}/location", new LocationRequest { Location = location }, ct);

    public Task<DeleteChestResponse?> DeleteChestAsync(int id, CancellationToken ct = default)
        => _api.DeleteAsync<DeleteChestResponse>($"/api/chests/{id}", ct);

    public async Task<List<ShowDto>> GetChestShowsAsync(int id, CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<ShowDto>>($"/api/chests/{id}/shows", ct);
        return result ?? new List<ShowDto>();
    }

    public Task<GenericResponse?> AddChestShowAsync(int chestId, int showId, CancellationToken ct = default)
        => _api.PostAsync<GenericResponse>($"/api/chests/{chestId}/shows/{showId}", null, ct);

    public Task<GenericResponse?> DeleteChestShowAsync(int chestId, int showId, CancellationToken ct = default)
        => _api.DeleteAsync<GenericResponse>($"/api/chests/{chestId}/shows/{showId}", ct);
    #endregion

    #region Items
    public async Task<List<ItemDto>> GetItemsAsync(CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<ItemDto>>("api/items/list", ct);
        return result ?? new List<ItemDto>();
    }

    public Task<ItemDto?> GetItemAsync(int id, CancellationToken ct = default)
        => _api.GetAsync<ItemDto>($"api/items/{id}", ct);

    public Task<AddItemResponse?> AddItemAsync(string name, CancellationToken ct = default)
        => _api.PostAsync<AddItemResponse>("api/items/add", new AddItemRequest { Name = name }, ct);

    public Task<ApiResponse?> UpdateItemAsync(int id, UpdateItemRequest request, CancellationToken ct = default)
        => _api.PutAsync<ApiResponse>($"api/items/{id}", request, ct);

    public Task<ApiResponse?> DeleteItemAsync(int id, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/items/{id}", ct);

    public Task<ApiResponse?> UpdateItemLocationAsync(int id, string location, CancellationToken ct = default)
        => _api.PutAsync<ApiResponse>($"api/items/{id}/location", new LocationRequest { Location = location }, ct);

    public async Task<List<ShowDto>> GetItemShowsAsync(int id, CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<ShowDto>>($"api/items/{id}/shows", ct);
        return result ?? new List<ShowDto>();
    }

    public Task<ApiResponse?> AddItemShowAsync(int itemId, int showId, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>($"api/items/{itemId}/shows/{showId}", null, ct);

    public Task<ApiResponse?> DeleteItemShowAsync(int itemId, int showId, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/items/{itemId}/shows/{showId}", ct);

    public async Task<List<ItemStallDto>> GetItemStallsAsync(int id, CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<ItemStallDto>>($"api/items/{id}/stalls", ct);
        return result ?? new List<ItemStallDto>();
    }

    public Task<ApiResponse?> AddItemStallAsync(int itemId, int stallId, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>($"api/items/{itemId}/stalls/{stallId}", null, ct);

    public Task<ApiResponse?> DeleteItemStallAsync(int itemId, int stallId, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/items/{itemId}/stalls/{stallId}", ct);

    public Task<ApiResponse?> AddItemChestAsync(int itemId, int chestId, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>($"api/items/{itemId}/chests/{chestId}", null, ct);

    public Task<ApiResponse?> DeleteItemChestAsync(int itemId, int chestId, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/items/{itemId}/chests/{chestId}", ct);

    public Task<ApiResponse?> AddItemKassetaAsync(int itemId, int kassetaId, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>($"api/items/{itemId}/kassety/{kassetaId}", null, ct);

    public Task<ApiResponse?> DeleteItemKassetaAsync(int itemId, int kassetaId, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/items/{itemId}/kassety/{kassetaId}", ct);

    public async Task<List<ItemPhotoDto>> GetItemPhotosAsync(int id, CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<ItemPhotoDto>>($"api/items/{id}/photos", ct);
        return result ?? new List<ItemPhotoDto>();
    }

    public Task<ApiResponse?> UploadItemPhotoAsync(int id, Stream stream, string fileName, CancellationToken ct = default)
    {
        var content = new StreamContent(stream);
        return _api.PostMultipartAsync<ApiResponse>($"api/items/{id}/photos", content, "photo", fileName, ct);
    }

    public Task<ApiResponse?> DeleteItemPhotoAsync(int itemId, double photoId, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/items/{itemId}/photos/{photoId}", ct);
    #endregion

    #region Shows
    public async Task<List<ShowDto>> GetShowsAsync(CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<ShowDto>>("api/shows/list", ct);
        return result ?? new List<ShowDto>();
    }

    public Task<ShowDto?> GetShowAsync(int id, CancellationToken ct = default)
        => _api.GetAsync<ShowDto>($"api/shows/{id}", ct);

    public async Task<List<ShowItemDto>> GetShowItemsAsync(int id, CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<ShowItemDto>>($"api/shows/{id}/items", ct);
        return result ?? new List<ShowItemDto>();
    }

    public Task<ApiResponse?> AddShowAsync(string name, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>("api/shows/add", new AddShowRequest { Name = name }, ct);

    public Task<ApiResponse?> UpdateShowAsync(int id, string name, CancellationToken ct = default)
        => _api.PutAsync<ApiResponse>($"api/shows/{id}", new UpdateShowRequest { Name = name }, ct);

    public Task<ApiResponse?> DeleteShowAsync(int id, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/shows/{id}", ct);
    #endregion

    #region Tasks
    public async Task<List<TaskDto>> GetTasksAsync(CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<TaskDto>>("api/tasks/list", ct);
        return result ?? new List<TaskDto>();
    }

    public async Task<List<int>> GetUserShowsAsync(int userId, CancellationToken ct = default)
    {
        var response = await _api.GetAsync<UserShowsResponse>($"api/tasks/user-shows/{userId}", ct);
        return response?.ShowIds ?? new List<int>();
    }

    public Task<NewTasksResponse?> GetNewTasksAsync(int userId, DateTime? since, CancellationToken ct = default)
    {
        var url = $"api/tasks/new/{userId}";
        if (since.HasValue)
            url += $"?since={Uri.EscapeDataString(since.Value.ToString("O"))}";
        return _api.GetAsync<NewTasksResponse>(url, ct);
    }

    public Task<TaskDto?> GetTaskAsync(int id, CancellationToken ct = default)
        => _api.GetAsync<TaskDto>($"api/tasks/{id}", ct);

    public Task<ApiResponse?> UpdateTaskStatusAsync(int id, string status, string? comment, CancellationToken ct = default)
        => _api.PatchAsync<ApiResponse>($"api/tasks/{id}/status", new TaskStatusRequest { Status = status, Comment = comment }, ct);

    public Task<ApiResponse?> UpdateTaskAsync(int id, TaskUpdateRequest request, CancellationToken ct = default)
        => _api.PutAsync<ApiResponse>($"api/tasks/{id}", request, ct);

    public Task<ApiResponse?> AddTaskAsync(TaskAddRequest request, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>("api/tasks/add", request, ct);

    public Task<ApiResponse?> DeleteTaskAsync(int id, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/tasks/{id}", ct);

    public async Task<List<TaskHistoryItem>> GetTaskHistoryAsync(int limit = 100, CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<TaskHistoryItem>>($"api/tasks/history?limit={limit}", ct);
        return result ?? new List<TaskHistoryItem>();
    }

    public async Task<List<TaskHistoryItem>> GetCompletedTaskHistoryAsync(int limit = 100, CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<TaskHistoryItem>>($"api/tasks/history/completed?limit={limit}", ct);
        return result ?? new List<TaskHistoryItem>();
    }
    #endregion

    #region Tape
    public Task<TapesListResponse?> GetTapesAsync(CancellationToken ct = default)
        => _api.GetAsync<TapesListResponse>("api/tape/list", ct);

    public Task<ApiResponse?> AddTapeAsync(int tapeId, int amount, string? reason, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>("api/tape/add", new TapeAddRequest { TapeId = tapeId, Amount = amount, Reason = reason }, ct);

    public Task<ApiResponse?> UseTapeAsync(int tapeId, int amount, string? reason, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>("api/tape/use", new TapeUseRequest { TapeId = tapeId, Amount = amount, Reason = reason }, ct);

    public Task<ApiResponse?> ReturnTapeAsync(int tapeId, int amount, string? reason, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>("api/tape/return", new TapeReturnRequest { TapeId = tapeId, Amount = amount, Reason = reason }, ct);

    public Task<ApiResponse?> UpdateTapeAsync(int tapeId, int quantity, string? reason, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>("api/tape/update", new TapeUpdateRequest { TapeId = tapeId, Quantity = quantity, Reason = reason }, ct);

    public async Task<List<TapeHistoryItem>> GetTapeHistoryAsync(CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<TapeHistoryItem>>("api/tape/history", ct);
        return result ?? new List<TapeHistoryItem>();
    }
    #endregion

    #region Kassety
    public async Task<List<KassetaDto>> GetKassetyAsync(CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<KassetaDto>>("api/kassety/list", ct);
        return result ?? new List<KassetaDto>();
    }

    public Task<AddKassetaResponse?> AddKassetaAsync(string number, string shelf, CancellationToken ct = default)
        => _api.PostAsync<AddKassetaResponse>("api/kassety/add", new AddKassetaRequest { Number = number, Shelf = shelf }, ct);

    public Task<ApiResponse?> UpdateKassetaAsync(int id, string number, string shelf, CancellationToken ct = default)
        => _api.PutAsync<ApiResponse>($"api/kassety/{id}", new AddKassetaRequest { Number = number, Shelf = shelf }, ct);

    public Task<ApiResponse?> DeleteKassetaAsync(int id, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/kassety/{id}", ct);

    public async Task<List<KassetaItemDto>> GetKassetaItemsAsync(int id, CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<KassetaItemDto>>($"api/kassety/{id}/items", ct);
        return result ?? new List<KassetaItemDto>();
    }

    public Task<ApiResponse?> AddKassetaItemAsync(int id, string name, int quantity, CancellationToken ct = default)
        => _api.PostAsync<ApiResponse>($"api/kassety/{id}/items", new AddKassetaItemRequest { Name = name, Quantity = quantity }, ct);

    public Task<ApiResponse?> UpdateKassetaItemAsync(int id, int itemId, string? name, int? quantity, CancellationToken ct = default)
        => _api.PutAsync<ApiResponse>($"api/kassety/{id}/items/{itemId}", new UpdateKassetaItemRequest { Name = name, Quantity = quantity }, ct);

    public Task<ApiResponse?> DeleteKassetaItemAsync(int id, int itemId, CancellationToken ct = default)
        => _api.DeleteAsync<ApiResponse>($"api/kassety/{id}/items/{itemId}", ct);
    #endregion

    #region Grafik
    public Task<ScheduleResponse?> GetScheduleAsync(string month, CancellationToken ct = default)
        => _api.GetAsync<ScheduleResponse>($"api/grafik/{month}", ct);

    public Task<ScheduleSaveResponse?> SaveScheduleAsync(string month, ScheduleMonth schedule, CancellationToken ct = default)
        => _api.PutAsync<ScheduleSaveResponse>($"api/grafik/{month}", new ScheduleSaveRequest { Schedule = schedule }, ct);
    #endregion

    #region Chat
    public async Task<List<ChatUserDto>> GetChatUsersAsync(CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<ChatUserDto>>("api/chat/users", ct);
        return result ?? new List<ChatUserDto>();
    }

    public async Task<List<ChatDto>> GetChatsAsync(CancellationToken ct = default)
        
    {
        var result = await _api.GetAsync<List<ChatDto>>("api/chat/chats", ct);
        return result ?? new List<ChatDto>();
    }

    public Task<CreateChatResponse?> CreateDirectChatAsync(int participantId, CancellationToken ct = default)
        => _api.PostAsync<CreateChatResponse>("api/chat/chats", new CreateChatRequest { ParticipantId = participantId }, ct);

    public Task<CreateChatResponse?> CreateGroupChatAsync(string name, List<int> participantIds, CancellationToken ct = default)
        => _api.PostAsync<CreateChatResponse>("api/chat/group", new CreateGroupChatRequest { Name = name, ParticipantIds = participantIds }, ct);

    public async Task<List<ChatMessageDto>> GetMessagesAsync(int chatId, int limit = 50, int? before = null, CancellationToken ct = default)
    {
        var url = $"api/chat/chats/{chatId}/messages?limit={limit}";
        if (before.HasValue)
            url += $"&before={before.Value}";
        var result = await _api.GetAsync<List<ChatMessageDto>>(url, ct);
        return result ?? new List<ChatMessageDto>();
    }

    public Task<ChatMessageDto?> SendMessageAsync(int chatId, string text, int? replyToId, CancellationToken ct = default)
        => _api.PostAsync<ChatMessageDto>($"api/chat/chats/{chatId}/messages", new SendMessageRequest { Text = text, ReplyToId = replyToId }, ct);

    public Task<ChatMessageDto?> EditMessageAsync(int chatId, int msgId, string text, CancellationToken ct = default)
        => _api.PutAsync<ChatMessageDto>($"api/chat/chats/{chatId}/messages/{msgId}", new EditMessageRequest { Text = text }, ct);

    public Task DeleteMessageAsync(int chatId, int msgId, CancellationToken ct = default)
        => _api.DeleteAsync<object>($"api/chat/chats/{chatId}/messages/{msgId}", ct);

    public Task<ChatMessageDto?> ForwardMessageAsync(int chatId, int originalMsgId, CancellationToken ct = default)
        => _api.PostAsync<ChatMessageDto>($"api/chat/chats/{chatId}/forward", new ForwardMessageRequest { OriginalMsgId = originalMsgId }, ct);

    public Task<ChatMessageDto?> UploadChatPhotoAsync(int chatId, Stream stream, string fileName, CancellationToken ct = default)
    {
        var content = new StreamContent(stream);
        return _api.PostMultipartAsync<ChatMessageDto>($"api/chat/chats/{chatId}/photo", content, "photo", fileName, ct);
    }

    public Task<ChatMessageDto?> UploadChatVoiceAsync(int chatId, Stream stream, string fileName, int duration, CancellationToken ct = default)
    {
        // TODO: duration as additional form field
        var content = new StreamContent(stream);
        return _api.PostMultipartAsync<ChatMessageDto>($"api/chat/chats/{chatId}/voice", content, "voice", fileName, ct);
    }

    public Task<ChatMessageDto?> ReactAsync(int chatId, int msgId, string emoji, CancellationToken ct = default)
        => _api.PostAsync<ChatMessageDto>($"api/chat/chats/{chatId}/messages/{msgId}/react", new ReactRequest { Emoji = emoji }, ct);

    public Task PinMessageAsync(int chatId, int? messageId, CancellationToken ct = default)
        => _api.PostAsync<object>($"api/chat/chats/{chatId}/pin", new PinRequest { MessageId = messageId }, ct);

    public Task<ChatInfoDto?> GetChatInfoAsync(int chatId, CancellationToken ct = default)
        => _api.GetAsync<ChatInfoDto>($"api/chat/chats/{chatId}", ct);

    public Task LeaveChatAsync(int chatId, CancellationToken ct = default)
        => _api.PostAsync<object>($"api/chat/chats/{chatId}/leave", null, ct);

    public Task RenameChatAsync(int chatId, string name, CancellationToken ct = default)
        => _api.PutAsync<object>($"api/chat/chats/{chatId}/name", new RenameRequest { Name = name }, ct);

    public Task<UserProfileDto?> GetUserProfileAsync(int id, CancellationToken ct = default)
        => _api.GetAsync<UserProfileDto>($"api/chat/users/{id}/profile", ct);

    public Task AddChatMemberAsync(int chatId, int userId, CancellationToken ct = default)
        => _api.PostAsync<object>($"api/chat/chats/{chatId}/members", new AddMemberRequest { UserId = userId }, ct);

    public Task RemoveChatMemberAsync(int chatId, int memberId, CancellationToken ct = default)
        => _api.DeleteAsync<object>($"api/chat/chats/{chatId}/members/{memberId}", ct);

    public Task MarkReadAsync(int chatId, List<int> messageIds, CancellationToken ct = default)
        => _api.PostAsync<object>($"api/chat/chats/{chatId}/read", new MarkReadRequest { MessageIds = messageIds }, ct);

    public async Task<List<UnreadChatSummaryDto>> GetUnreadSummaryAsync(DateTime? since, CancellationToken ct = default)
    {
        var url = "api/chat/unread-summary";
        if (since.HasValue)
            url += $"?since={Uri.EscapeDataString(since.Value.ToString("O"))}";
        var result = await _api.GetAsync<List<UnreadChatSummaryDto>>(url, ct);
        return result ?? new List<UnreadChatSummaryDto>();
    }
    #endregion
}
