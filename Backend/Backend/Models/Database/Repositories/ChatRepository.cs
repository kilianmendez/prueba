using Backend.Models.Database.Entities;
using Backend.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Interfaces;

public class ChatRepository : IChatRepository
{
    private readonly DataContext _context;

    public ChatRepository(DataContext context)
    => _context = context;

    public async Task<List<ChatDTO>> GetUserChatsAsync(Guid userId)
    {
        var follows = await _context.Follows
            .Include(f => f.Following)
            .Where(f => f.FollowerId == userId)
            .ToListAsync();

        var chats = new List<ChatDTO>(follows.Count);

        foreach (var follow in follows)
        {
            var other = follow.Following;

            var msgs = await _context.Messages
                .Where(m =>
                    (m.SenderId == userId && m.ReceiverId == other.Id) ||
                    (m.SenderId == other.Id && m.ReceiverId == userId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            var last = msgs.LastOrDefault();

            chats.Add(new ChatDTO
            {
                OtherUserId = other.Id,
                OtherUserName = other.Name,
                OtherUserAvatar = other.AvatarUrl,
                LastMessage = last?.Content ?? "Aún no hay mensajes",
                LastMessageAt = last?.SentAt ?? follow.CreatedAt
            });
        }

        return chats
            .OrderByDescending(c => c.LastMessageAt)
            .ToList();
    }

    public async Task<List<MessageDTO>> GetMessagesBetweenUsersAsync(Guid userA, Guid userB)
    {
        var messages = await _context.Messages
                .Include(m => m.Sender)
                .Where(m =>
                    (m.SenderId == userA && m.ReceiverId == userB) ||
                    (m.SenderId == userB && m.ReceiverId == userA))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

        return messages.Select(m => new MessageDTO
        {
            Id = m.Id,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            Content = m.Content,
            MessageType = m.MessageType,
            SentAt = m.SentAt,
            Status = m.Status,
            SenderName = m.SenderId != userA
                             ? m.Sender.Name
                             : null,
            SenderAvatar = m.SenderId != userA
                             ? m.Sender.AvatarUrl
                             : null
        }).ToList();
    }
}


