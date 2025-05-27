using Backend.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Database.Repositories
{
    public class EventRepository : Repository<Event>
    {
        private readonly DataContext _context;
        public EventRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Event?> GetByIdWithRelationsAsync(Guid id)
        {
            return await _context.Events
                .Include(e => e.Creator)
                .Include(e => e.Participants)
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Event>> GetAllWithRelationsAsync()
        {
            return await _context.Events
                .Include(e => e.Creator)
                .Include(e => e.Participants)
                .ToListAsync();
        }
    }
}
