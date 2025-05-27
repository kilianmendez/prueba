using Backend.Models.Database.Entities;
using Backend.Models.Database.Enum;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Database.Repositories
{
    public class ReservationRepository : Repository<Reservation>
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Reservation?> GetByIdAsync(Guid id)
        {
            return await _context.Reservations.FindAsync(id);
        }
        public async Task<IEnumerable<Reservation>> GetReservationsByAccommodationIdAsync(Guid accommodationId)
        {
            return await _context.Reservations
                .Where(r => r.AccommodationId == accommodationId && r.Status != ReservationStatus.Cancelled)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task InsertAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
        }
    }
}
