using Backend.Models.Database.Entities; 
using Backend.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Database.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public FollowRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<bool> AddFollowAsync(Guid followerId, Guid followingId)
        {
            if (followerId == Guid.Empty)
                throw new Exception("Falta el identificador del seguidor.");
            if (followingId == Guid.Empty)
                throw new Exception("Falta el identificador del usuario a seguir.");

            try
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                using DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();

                bool exists = await context.Follows.AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);
                if (exists)
                    throw new Exception("Ya sigues a este usuario.");

                var follow = new Follow
                {
                    FollowerId = followerId,
                    FollowingId = followingId,
                    CreatedAt = DateTime.UtcNow
                };

                context.Follows.Add(follow);
                int changes = await context.SaveChangesAsync();
                return changes > 0;
            }
            catch (DbUpdateException)
            {
                throw new Exception("No se pudo completar el seguimiento. Inténtalo de nuevo más tarde.");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Ya sigues a este usuario.")
                    throw; 
                else
                    throw new Exception("Ocurrió un error al intentar seguir al usuario. Por favor, inténtalo más tarde.");
            }
        }
    }
}
