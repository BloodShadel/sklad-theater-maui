namespace SkladTheater.Maui.Models;

public class NewTaskMessage
{
    public TaskDto Task { get; set; } = new();
}

public class ChatMessageReceived
{
    public ChatMessageDto Message { get; set; } = new();
}

public class MessagesReadReceived
{
    public int ChatId { get; set; }
    public int ReadBy { get; set; }
    public List<int> MessageIds { get; set; } = new();
}

public class OnlineUsersReceived
{
    public List<int> UserIds { get; set; } = new();
}

public class UserOnlineReceived
{
    public int UserId { get; set; }
}

public class UserOfflineReceived
{
    public int UserId { get; set; }
}

public class TypingReceived
{
    public int ChatId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
}

public class MessageEditedReceived
{
    public ChatMessageDto Message { get; set; } = new();
}

public class MessageDeletedReceived
{
    public int ChatId { get; set; }
    public int MessageId { get; set; }
}

public class PinChangedReceived
{
    public int ChatId { get; set; }
    public int? MessageId { get; set; }
}

public class ChatListUpdateReceived { }

public class DataChangedReceived
{
    public string Collection { get; set; } = string.Empty;
}
