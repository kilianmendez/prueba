using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database.Entities;

public class ForumThread
{
    [Key]
    public Guid Id { get; set; }
    public Guid ForumId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public Forum Forum { get; set; } = null!;
    public ICollection<ForumMessages> Posts { get; set; } = new List<ForumMessages>();
}
