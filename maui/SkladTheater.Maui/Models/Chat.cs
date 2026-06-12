using Newtonsoft.Json;

namespace SkladTheater.Maui.Models;

public class ChatUserDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("displayName")] public string DisplayName { get; set; } = string.Empty;
    [JsonProperty("avatarUrl")] public string? AvatarUrl { get; set; }
}

public class LastMessageDto
{
    [JsonProperty("text")] public string? Text { get; set; }
    [JsonProperty("senderId")] public int SenderId { get; set; }
    [JsonProperty("createdAt")] public string CreatedAt { get; set; } = string.Empty;
    [JsonProperty("readBy")] public List<int> ReadBy { get; set; } = new();
}

public class ChatDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("participants")] public List<int> Participants { get; set; } = new();
    [JsonProperty("otherUsers")] public List<ChatUserDto> OtherUsers { get; set; } = new();
    [JsonProperty("lastMessage")] public LastMessageDto? LastMessage { get; set; }
    [JsonProperty("unreadCount")] public int UnreadCount { get; set; }
    [JsonProperty("updatedAt")] public string UpdatedAt { get; set; } = string.Empty;
    [JsonProperty("type")] public string? Type { get; set; }
    [JsonProperty("pinnedMessageId")] public int? PinnedMessageId { get; set; }

    public string DisplayName => string.IsNullOrWhiteSpace(Name) && OtherUsers.Any()
        ? string.Join(", ", OtherUsers.Select(u => u.DisplayName))
        : Name;
}

public class ReactionGroupDto
{
    [JsonProperty("emoji")] public string Emoji { get; set; } = string.Empty;
    [JsonProperty("count")] public int Count { get; set; }
    [JsonProperty("userIds")] public List<int> UserIds { get; set; } = new();
}

public class ReplyDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("senderName")] public string SenderName { get; set; } = string.Empty;
    [JsonProperty("text")] public string? Text { get; set; }
    [JsonProperty("imageUrl")] public string? ImageUrl { get; set; }
}

public class ChatMessageDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("chatId")] public int ChatId { get; set; }
    [JsonProperty("senderId")] public int SenderId { get; set; }
    [JsonProperty("senderName")] public string? SenderName { get; set; }
    [JsonProperty("senderAvatarUrl")] public string? SenderAvatarUrl { get; set; }
    [JsonProperty("text")] public string? Text { get; set; }
    [JsonProperty("imageUrl")] public string? ImageUrl { get; set; }
    [JsonProperty("audioUrl")] public string? AudioUrl { get; set; }
    [JsonProperty("audioDuration")] public int? AudioDuration { get; set; }
    [JsonProperty("createdAt")] public string CreatedAt { get; set; } = string.Empty;
    [JsonProperty("readBy")] public List<int> ReadBy { get; set; } = new();
    [JsonProperty("deleted")] public bool Deleted { get; set; }
    [JsonProperty("edited")] public bool Edited { get; set; }
    [JsonProperty("replyToId")] public int? ReplyToId { get; set; }
    [JsonProperty("replyTo")] public ReplyDto? ReplyTo { get; set; }
    [JsonProperty("reactions")] public List<ReactionGroupDto> Reactions { get; set; } = new();
    [JsonProperty("forwarded")] public bool Forwarded { get; set; }

    public bool IsFromCurrentUser(int currentUserId) => SenderId == currentUserId;
}

public class ChatInfoDto
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("type")] public string Type { get; set; } = string.Empty;
    [JsonProperty("participants")] public List<ChatUserDto> Participants { get; set; } = new();
    [JsonProperty("pinnedMessageId")] public int? PinnedMessageId { get; set; }
    [JsonProperty("creatorId")] public int? CreatorId { get; set; }
}

public class UnreadChatSummaryDto
{
    [JsonProperty("chatId")] public int ChatId { get; set; }
    [JsonProperty("chatName")] public string ChatName { get; set; } = string.Empty;
    [JsonProperty("unreadCount")] public int UnreadCount { get; set; }
    [JsonProperty("lastText")] public string? LastText { get; set; }
    [JsonProperty("lastSenderName")] public string LastSenderName { get; set; } = string.Empty;
    [JsonProperty("updatedAt")] public string UpdatedAt { get; set; } = string.Empty;
}

public class CreateChatRequest
{
    [JsonProperty("participantId")] public int ParticipantId { get; set; }
}

public class CreateGroupChatRequest
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("participantIds")] public List<int> ParticipantIds { get; set; } = new();
}

public class CreateChatResponse
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("type")] public string? Type { get; set; }
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("participants")] public List<int>? Participants { get; set; }
    [JsonProperty("createdAt")] public string? CreatedAt { get; set; }
}

public class SendMessageRequest
{
    [JsonProperty("text")] public string Text { get; set; } = string.Empty;
    [JsonProperty("replyToId")] public int? ReplyToId { get; set; }
}

public class EditMessageRequest
{
    [JsonProperty("text")] public string Text { get; set; } = string.Empty;
}

public class MarkReadRequest
{
    [JsonProperty("messageIds")] public List<int> MessageIds { get; set; } = new();
}

public class ForwardMessageRequest
{
    [JsonProperty("originalMsgId")] public int OriginalMsgId { get; set; }
}

public class ReactRequest
{
    [JsonProperty("emoji")] public string Emoji { get; set; } = string.Empty;
}

public class PinRequest
{
    [JsonProperty("messageId")] public int? MessageId { get; set; }
}

public class RenameRequest
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
}

public class AddMemberRequest
{
    [JsonProperty("userId")] public int UserId { get; set; }
}
