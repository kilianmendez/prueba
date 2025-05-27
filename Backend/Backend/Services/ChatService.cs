using Backend.Models.Database.Entities;
using Backend.Models.Dtos;
using Backend.Models.Interfaces;
using Stripe;

namespace Backend.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;

    public ChatService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public Task<List<ChatDTO>> ObtenerChatsDeUsuarioAsync(Guid userId)
    {
       var chats = _chatRepository.GetUserChatsAsync(userId);
        return chats;
    }

    public Task<List<MessageDTO>> GetChatHistoryAsync(Guid userId, Guid contactId)
    {
        var chatHistory = _chatRepository.GetMessagesBetweenUsersAsync(userId, contactId);
        return chatHistory;
    }
}
