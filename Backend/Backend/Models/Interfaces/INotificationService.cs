namespace Backend.Models.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(string userId, string message, string action = "notification");
}
