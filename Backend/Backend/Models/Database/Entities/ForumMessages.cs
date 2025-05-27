using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database.Entities;

public class ForumMessages
{
    [Key]
    public Guid Id { get; set; }
    public Guid ThreadId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public Guid? ParentMessageId { get; set; }

    public ForumThread Thread { get; set; } = null!;
    
    public ForumMessages? ParentMessage { get; set; }
    
    public ICollection<ForumMessages> Replies { get; set; } = new List<ForumMessages>();
}
