using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class AssignedUser
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("display_name")] public string DisplayName { get; set; } = string.Empty;
}

public class TaskDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("title")] public string Title { get; set; } = string.Empty;
    [JsonProperty("description")] public string? Description { get; set; }
    [JsonProperty("type")] public string? Type { get; set; }
    [JsonProperty("status")] public string Status { get; set; } = string.Empty;
    [JsonProperty("priority")] public string? Priority { get; set; }
    [JsonProperty("assigned_to")] public int? AssignedTo { get; set; }
    [JsonProperty("assigned_user")] public AssignedUser? AssignedUser { get; set; }
    [JsonProperty("assigned_users")] public List<AssignedUser>? AssignedUsers { get; set; }
    [JsonProperty("show_id")] public int? ShowId { get; set; }
    [JsonProperty("show_name")] public string? ShowName { get; set; }
    [JsonProperty("destination")] public string? Destination { get; set; }
    [JsonProperty("due_date")] public string? DueDate { get; set; }
    [JsonProperty("created_at")] public string? CreatedAt { get; set; }
}

public class TaskStatusRequest
{
    [JsonProperty("status")] public string Status { get; set; } = string.Empty;
    [JsonProperty("comment")] public string? Comment { get; set; }
}

public class TaskUpdateRequest
{
    [JsonProperty("title")] public string? Title { get; set; }
    [JsonProperty("description")] public string? Description { get; set; }
    [JsonProperty("assignedTo")] public int? AssignedTo { get; set; }
    [JsonProperty("assignedUsers")] public List<int>? AssignedUsers { get; set; }
    [JsonProperty("priority")] public string? Priority { get; set; }
    [JsonProperty("status")] public string? Status { get; set; }
    [JsonProperty("dueDate")] public string? DueDate { get; set; }
}

public class TaskAddRequest
{
    [JsonProperty("title")] public string Title { get; set; } = string.Empty;
    [JsonProperty("description")] public string? Description { get; set; }
    [JsonProperty("assignedTo")] public int? AssignedTo { get; set; }
    [JsonProperty("assignedUsers")] public List<int>? AssignedUsers { get; set; }
    [JsonProperty("priority")] public string? Priority { get; set; }
    [JsonProperty("dueDate")] public string? DueDate { get; set; }
    [JsonProperty("type")] public string? Type { get; set; }
    [JsonProperty("destination")] public string? Destination { get; set; }
    [JsonProperty("showId")] public int? ShowId { get; set; }
}

public class TaskHistoryItem
{
    [JsonProperty("id")] public long Id { get; set; }
    [JsonProperty("task_id")] public int TaskId { get; set; }
    [JsonProperty("task_title")] public string TaskTitle { get; set; } = string.Empty;
    [JsonProperty("task_type")] public string? TaskType { get; set; }
    [JsonProperty("old_status")] public string OldStatus { get; set; } = string.Empty;
    [JsonProperty("new_status")] public string NewStatus { get; set; } = string.Empty;
    [JsonProperty("user_id")] public int UserId { get; set; }
    [JsonProperty("user")] public AssignedUser? User { get; set; }
    [JsonProperty("comment")] public string? Comment { get; set; }
    [JsonProperty("timestamp")] public string Timestamp { get; set; } = string.Empty;
}

public class UserShowsResponse
{
    [JsonProperty("showIds")] public List<int> ShowIds { get; set; } = new();
}

public class NewTasksResponse
{
    [JsonProperty("tasks")] public List<TaskDto> Tasks { get; set; } = new();
    [JsonProperty("count")] public int Count { get; set; }
}
