namespace Backend.Models.Dtos;

public class MessageDTO
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid? ReceiverId { get; set; }
    public string Content { get; set; }
    public string MessageType { get; set; }
    public DateTime SentAt { get; set; }
    public string Status { get; set; }

    public string? SenderName { get; set; }
    public string? SenderAvatar { get; set; }
}