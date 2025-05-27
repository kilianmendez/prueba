using Backend.Models.Database.Enum;

namespace Backend.Models.Dtos
{
    public class ReservationUpdateRequest
    {
        public ReservationStatus? Status { get; set; }
    }
}
