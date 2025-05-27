using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Database.Enum;

namespace Backend.Models.Database.Entities;

public class Review
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }
    public Rating Rating { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("Reservation")]
    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }
}
