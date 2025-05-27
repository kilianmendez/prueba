using Backend.Models.Interfaces;
using Backend.WebSockets;
using System.Text.Json;

namespace Backend.Services;

public class NotificationService : INotificationService
{
    private readonly WebsocketHandler _websocketHandler;

    public NotificationService(WebsocketHandler websocketHandler)
    {
        _websocketHandler = websocketHandler;
    }

    public async Task SendNotificationAsync(string userId, string message, string action = "notification")
    {
        var payload = new
        {
            success = true,
            action = action,
            message = message
        };

        await _websocketHandler.SendMessageToUser(userId, JsonSerializer.Serialize(payload));
    }
}
