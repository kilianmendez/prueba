using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Database.Entities;

public class Follow
{
    [Key]
    public Guid FollowId { get; set; }
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(FollowerId))]
    public User Follower { get; set; }

    [ForeignKey(nameof(FollowingId))]
    public User Following { get; set; }
}
