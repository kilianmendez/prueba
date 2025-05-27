using Backend.Models.Database.Enum;

namespace Backend.Models.Dtos;

public class ReviewCreateDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Rating Rating { get; set; }
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
}
