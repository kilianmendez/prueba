using Backend.Models.Database.Entities;
using Backend.Models.Database.Enum;
using Backend.Models.Dtos;

namespace Backend.Models.Mappers
{
    public class ReservationMapper
    {
        public ReservationMapper() { }
        public static ReservationDto ToDto(Reservation reservation)
        {
            if (reservation == null) return null;

            return new ReservationDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                AccommodationId = reservation.AccommodationId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status,
                TotalPrice = reservation.TotalPrice
            };
        }
        public static Reservation ToEntity(ReservationCreateRequest request)
        {
            return new Reservation
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                AccommodationId = request.AccommodationId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = ReservationStatus.Pending,
                TotalPrice = request.TotalPrice,
                
              
            };
        }
        public static void UpdateEntity(Reservation reservation, ReservationUpdateRequest request)
        {
            if (request.Status.HasValue)
                reservation.Status = request.Status.Value;
        }
    }
}
