using Backend.Models.Database.Entities;
using Backend.Models.Interfaces;
using Backend.WebSockets;
using System.Text.Json;

namespace Backend.Services;

public class MessagesService : IMessagesService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly WebsocketHandler _websocketHandler;

    public MessagesService(IServiceScopeFactory serviceScopeFactory, WebsocketHandler websocketHandler)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _websocketHandler = websocketHandler;
    }

    public async Task<bool> SendMessageAsync(Guid senderId, Guid receiverId, string content)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var messagesRepository = scope.ServiceProvider.GetRequiredService<IMessagesRepository>();

        var message = new Messages
        {
            Id = Guid.NewGuid(),
            SenderId = senderId,
            ReceiverId = receiverId,
            Content = content,
            MessageType = "text",
            SentAt = DateTime.UtcNow,
            Status = "sent"
        };

        await messagesRepository.AddMessageAsync(message);
        await messagesRepository.SaveChangesAsync();

        bool isDelivered = await _websocketHandler.SendMessageToUser(
            receiverId.ToString(),
            JsonSerializer.Serialize(new
            {
                action = "new_message",
                messageId = message.Id,
                senderId,
                receiverId,
                content,
                sentAt = message.SentAt,
                status = "unread"
            }));

        if (isDelivered)
        {
            message.Status = "delivered";
            await messagesRepository.SaveChangesAsync();

            await _websocketHandler.SendMessageToUser(
                senderId.ToString(),
                JsonSerializer.Serialize(new
                {
                    action = "message_status",
                    messageId = message.Id,
                    status = "delivered"
                }));
        }
        else
        {
            message.Status = "not_received";
            await messagesRepository.SaveChangesAsync();

            await _websocketHandler.SendMessageToUser(
                senderId.ToString(),
                JsonSerializer.Serialize(new
                {
                    action = "message_status",
                    messageId = message.Id,
                    status = "not_received"
                }));
        }

        return true;
    }

    public async Task MarkMessagesAsReadAsync(Guid userId, Guid contactId)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var messagesRepository = scope.ServiceProvider.GetRequiredService<IMessagesRepository>();

        var chatHistory = await messagesRepository.GetMessagesBetweenUsersAsync(contactId, userId);
        var unreadMessages = chatHistory.FindAll(m => m.Status != "read");

        if (unreadMessages.Count > 0)
        {
            foreach (var message in unreadMessages)
            {
                message.Status = "read";
                await _websocketHandler.SendMessageToUser(
                    contactId.ToString(),
                    JsonSerializer.Serialize(new
                    {
                        action = "message_status",
                        messageId = message.Id,
                        status = "read"
                    }));
            }
            await messagesRepository.SaveChangesAsync();
        }
    }

    public async Task SendPendingMessagesAsync(string userId)
    {
        if (!Guid.TryParse(userId, out Guid receiverId))
            return;

        using var scope = _serviceScopeFactory.CreateScope();
        var messagesRepository = scope.ServiceProvider.GetRequiredService<IMessagesRepository>();

        var pendingMessages = await messagesRepository.GetPendingMessagesAsync(receiverId);

        foreach (var message in pendingMessages)
        {
            bool isDelivered = await _websocketHandler.SendMessageToUser(
                receiverId.ToString(),
                JsonSerializer.Serialize(new
                {
                    action = "new_message",
                    messageId = message.Id,
                    senderId = message.SenderId,
                    receiverId = message.ReceiverId,
                    content = message.Content,
                    sentAt = message.SentAt,
                    status = "unread"
                }));

            if (isDelivered)
            {
                message.Status = "delivered";
                await messagesRepository.SaveChangesAsync();

                await _websocketHandler.SendMessageToUser(
                    message.SenderId.ToString(),
                    JsonSerializer.Serialize(new
                    {
                        action = "message_status",
                        messageId = message.Id,
                        status = "delivered"
                    }));
            }
        }
    }

    public Task<List<Messages>> GetChatHistoryAsync(Guid userId, Guid contactId)
    {
        throw new NotImplementedException();
    }
}
