namespace Backend.Models.Dtos;

public class ReviewDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDTO User { get; set; }
    public ReservationDTO Reservation { get; set; }
}

public class UserDTO
{
    public string Name { get; set; }   
    public string LastName { get; set; }
    public string AvatarUrl { get; set; }
}

public class ReservationDTO
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
