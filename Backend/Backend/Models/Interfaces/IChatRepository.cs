using Backend.Models.Dtos;

namespace Backend.Models.Interfaces;

public interface IChatRepository
{
    Task<List<MessageDTO>> GetMessagesBetweenUsersAsync(Guid userA, Guid userB);

    Task<List<ChatDTO>> GetUserChatsAsync(Guid userId);
}