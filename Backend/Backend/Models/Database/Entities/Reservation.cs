using System.ComponentModel.DataAnnotations.Schema;
using Backend.Models.Database.Enum;

namespace Backend.Models.Database.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        //Relaciones
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Accommodation")]
        public Guid AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }

    }
}
