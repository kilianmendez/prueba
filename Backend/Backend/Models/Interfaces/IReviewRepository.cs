using Backend.Models.Database.Entities;
using Backend.Models.Dtos;

namespace Backend.Models.Interfaces;

public interface IReviewRepository
{
    Task CreateReviewAsync(Review review);
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review> GetReviewByIdAsync(Guid id);
    Task<IEnumerable<Review>> GetReviewsByAccommodationIdAsync(Guid accommodationId);
    Task DeleteReviewAsync(Guid id);
    Task<Review> GetReviewByReservationAndUserAsync(Guid reservationId, Guid userId);

}
