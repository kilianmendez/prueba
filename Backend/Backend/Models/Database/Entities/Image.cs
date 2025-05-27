using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Database.Entities;

public class Image
{
    [Key]
    public Guid Id { get; set; }
    public required string Url { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    //Relaciones
    [ForeignKey(nameof(User))]
    public Guid? UserId { get; set; }
    public User User { get; set; }

    [ForeignKey(nameof(Recommendation))]
    public Guid? RecommendationId { get; set; }
    public Recommendation Recommendation { get; set; }
}
