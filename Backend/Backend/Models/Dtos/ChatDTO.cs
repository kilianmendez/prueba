namespace Backend.Models.Dtos;

public class ChatDTO
{
    public Guid OtherUserId { get; set; }
    public string OtherUserName { get; set; } = "";
    public string OtherUserAvatar { get; set; } = "";
    public string LastMessage { get; set; } = "";
    public DateTime LastMessageAt { get; set; }
}