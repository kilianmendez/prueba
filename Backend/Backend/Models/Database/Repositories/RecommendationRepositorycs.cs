using Backend.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Database.Repositories
{
    public class RecommendationRepository : Repository<Recommendation>
    {
        private readonly DataContext _context;
        public RecommendationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Recommendation?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Recommendation>()
                .Include(r => r.RecommendationImages)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Recommendation>> GetAllAsync()
        {
            return await _context.Set<Recommendation>()
                .Include(r => r.RecommendationImages)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllCountriesAsync()
        {
            return await _context.Recommendations
                .Where(a => !string.IsNullOrEmpty(a.Country))
                .Select(a => a.Country)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetCitiesByCountryAsync(string country)
        {
            return await _context.Recommendations
                .Where(a => a.Country == country && !string.IsNullOrEmpty(a.City))
                .Select(a => a.City)
                .Distinct()
                .ToListAsync();
        }

        public async Task InsertAsync(Recommendation recommendation)
        {
            await _context.Set<Recommendation>().AddAsync(recommendation);
        }

        public async Task UpdateAsync(Recommendation recommendation)
        {
            _context.Set<Recommendation>().Update(recommendation);
        }

        public async Task DeleteAsync(Recommendation recommendation)
        {
            _context.Set<Recommendation>().Remove(recommendation);
        }
    }
}
