using Backend.Models.Database.Entities;
using Backend.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Database.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly DataContext _context;

    public ReviewRepository(DataContext context)
    {
        _context = context;
    }

    public async Task CreateReviewAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Reservation)
            .ToListAsync();
    }

    public async Task<Review> GetReviewByIdAsync(Guid id)
    {
        return await _context.Reviews.FindAsync(id);
    }

    public async Task DeleteReviewAsync(Guid id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Review> GetReviewByReservationAndUserAsync(Guid reservationId, Guid userId)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(r => r.ReservationId == reservationId && r.UserId == userId);
    }

    public async Task<IEnumerable<Review>> GetReviewsByAccommodationIdAsync(Guid accommodationId)
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Reservation)
            .Where(r => r.Reservation.AccommodationId == accommodationId)
            .ToListAsync();
    }
}
