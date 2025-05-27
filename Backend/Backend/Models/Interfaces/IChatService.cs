using Backend.Models.Database.Entities;
using Backend.Models.Dtos;

namespace Backend.Models.Interfaces;

public interface IChatService
{
    Task<List<ChatDTO>> ObtenerChatsDeUsuarioAsync(Guid userId);
    Task<List<MessageDTO>> GetChatHistoryAsync(Guid userId, Guid contactId);

}