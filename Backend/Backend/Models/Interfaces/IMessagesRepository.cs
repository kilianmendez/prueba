using Backend.Models.Database.Entities;

namespace Backend.Models.Interfaces;

public interface IMessagesRepository
{
    Task AddMessageAsync(Messages message);
    Task<List<Messages>> GetPendingMessagesAsync(Guid receiverId);
    Task<List<Messages>> GetMessagesBetweenUsersAsync(Guid userId1, Guid userId2);
    Task SaveChangesAsync();
}
