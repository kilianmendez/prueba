using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Dtos
{
    public class ReservationCreateRequest
    {
        public required Guid UserId { get; set; }

        public required Guid AccommodationId { get; set; }

        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; }

        public required decimal TotalPrice { get; set; }
    }
}
