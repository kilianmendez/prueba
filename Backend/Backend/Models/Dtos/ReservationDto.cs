using Backend.Models.Database.Enum;

namespace Backend.Models.Dtos
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AccommodationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
