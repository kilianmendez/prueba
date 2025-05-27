using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using Backend.Models.Interfaces;
using Backend.Services;

namespace Backend.WebSockets;

public class WebsocketHandler
{
    private static readonly ConcurrentDictionary<string, WebSocket> _connections = new();

    public async Task HandleAsync(HttpContext context, WebSocket webSocket)
    {
        var userId = context.Items["userId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
        {
            await webSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Falta el userId", CancellationToken.None);
            return;
        }

        try
        {
            _connections[userId] = webSocket;
            Console.WriteLine($"🔗 Usuario {userId} conectado");
            var messagesService = context.RequestServices.GetRequiredService<IMessagesService>();
            await messagesService.SendPendingMessagesAsync(userId);


            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Cierre normal", CancellationToken.None);
                    break;
                }

                if (result.MessageType != WebSocketMessageType.Text)
                    continue;

                try
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var request = JsonSerializer.Deserialize<Dictionary<string, string>>(message);

                    if (request != null && request.TryGetValue("action", out string action))
                    {
                        switch (action)
                        {
                            case "follow":
                                if (request.TryGetValue("targetUserId", out string targetUserIdStr) &&
                                    Guid.TryParse(targetUserIdStr, out Guid targetId) &&
                                    Guid.TryParse(userId, out Guid senderId))
                                {
                                    var followService = context.RequestServices.GetRequiredService<IFollowService>();
                                    await followService.FollowUserAsync(senderId, targetId);
                                }
                                else
                                {
                                    var errorResponseParameters = new
                                    {
                                        success = false,
                                        action = "follow",
                                        message = "Parámetros inválidos para follow."
                                    };
                                    await SendMessageToUser(userId, JsonSerializer.Serialize(errorResponseParameters));
                                }
                                break;

                            case "send_message":
                                if (request.TryGetValue("receiverId", out string receiverIdStr) &&
                                    Guid.TryParse(receiverIdStr, out Guid receiverId) &&
                                    Guid.TryParse(userId, out Guid senderIdChat) &&
                                    request.TryGetValue("content", out string content))
                                {
                                    var messageService = context.RequestServices.GetRequiredService<IMessagesService>();
                                    await messageService.SendMessageAsync(senderIdChat, receiverId, content);
                                }
                                else
                                {
                                    var errorResponseParameters = new
                                    {
                                        success = false,
                                        action = "send_message",
                                        message = "Parámetros inválidos para send_message."
                                    };
                                    await SendMessageToUser(userId, JsonSerializer.Serialize(errorResponseParameters));
                                }
                                break;

                            case "mark_as_read":
                                if (request.TryGetValue("contactId", out string contactIdMarkStr) &&
                                    Guid.TryParse(contactIdMarkStr, out Guid contactIdMark) &&
                                    Guid.TryParse(userId, out Guid currentUserIdMark))
                                {
                                    var messageService = context.RequestServices.GetRequiredService<IMessagesService>();
                                    await messageService.MarkMessagesAsReadAsync(currentUserIdMark, contactIdMark);
                                    var response = new
                                    {
                                        action = "mark_as_read",
                                        message = "Mensajes marcados como leídos."
                                    };
                                    await SendMessageToUser(userId, JsonSerializer.Serialize(response));
                                }
                                else
                                {
                                    var errorResponseParameters = new
                                    {
                                        success = false,
                                        action = "mark_as_read",
                                        message = "Parámetros inválidos para mark_as_read."
                                    };
                                    await SendMessageToUser(userId, JsonSerializer.Serialize(errorResponseParameters));
                                }
                                break;

                            default:
                                var errorResponse = new
                                {
                                    success = false,
                                    message = "❌ Acción no reconocida."
                                };
                                await SendMessageToUser(userId, JsonSerializer.Serialize(errorResponse));
                                break;
                        }
                    }
                    else
                    {
                        var errorResponse = new
                        {
                            success = false,
                            message = "❌ Mensaje inválido o acción no especificada."
                        };
                        await SendMessageToUser(userId, JsonSerializer.Serialize(errorResponse));
                    }
                }
                catch (JsonException)
                {
                    var errorResponse = new
                    {
                        success = false,
                        message = "❌ Formato JSON inválido."
                    };
                    await SendMessageToUser(userId, JsonSerializer.Serialize(errorResponse));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error en WebSocket para usuario {userId}: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
        }
        finally
        {
            _connections.TryRemove(userId, out _);
            if (webSocket.State == WebSocketState.Open)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Cierre en finally", CancellationToken.None);
            }
            Console.WriteLine($"❌ Usuario {userId} desconectado");

        }
    }

    public async Task<bool> SendMessageToUser(string userId, string message)
    {
        if (_connections.TryGetValue(userId, out var webSocket) && webSocket.State == WebSocketState.Open)
        {
            try
            {
                var response = Encoding.UTF8.GetBytes(message);
                await webSocket.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error enviando mensaje al usuario {userId}: {ex.Message}");
            }
        }
        return false;
    }
}
