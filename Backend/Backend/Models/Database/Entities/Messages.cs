namespace Backend.Models.Database.Entities;

public class Messages
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid? ReceiverId { get; set; }
    public string Content { get; set; }
    public string MessageType { get; set; } = "text";
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "sent";


    public User Sender { get; set; }
    public User? Receiver { get; set; }
}
