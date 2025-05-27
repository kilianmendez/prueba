using Backend.Models.Database.Entities;

namespace Backend.Models.Interfaces;

public interface IMessagesService
{
    Task<List<Messages>> GetChatHistoryAsync(Guid userId, Guid contactId);
    Task<bool> SendMessageAsync(Guid senderId, Guid receiverId, string content);
    Task MarkMessagesAsReadAsync(Guid userId, Guid contactId);
    Task SendPendingMessagesAsync(string userId);
}
