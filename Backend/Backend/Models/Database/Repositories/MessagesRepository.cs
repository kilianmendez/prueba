using Backend.Models.Database.Entities;
using Backend.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Database.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly DataContext _context;

        public MessagesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(Messages message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task<List<Messages>> GetPendingMessagesAsync(Guid receiverId)
        {
            return await _context.Messages
                .Where(m => m.ReceiverId == receiverId && m.Status == "not_received")
                .ToListAsync();
        }

        public async Task<List<Messages>> GetMessagesBetweenUsersAsync(Guid userId1, Guid userId2)
        {
            return await _context.Messages
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                            (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
