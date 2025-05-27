using Backend.Models.Database.Enum;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database.Entities;

public class Forum
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Country { get; set; } = null!;
    public ForumCategory Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public ICollection<ForumThread> Threads { get; set; } = new List<ForumThread>();
}
