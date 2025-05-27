using Backend.Models.Database;
using Backend.Models.Database.Repositories;
using Backend.Models.Interfaces;

namespace Backend.Services;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;
    private readonly INotificationService _notificationService;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FollowService(
        IFollowRepository followRepository,
        INotificationService notificationService,
        IServiceScopeFactory serviceScopeFactory)
    {
        _followRepository = followRepository;
        _notificationService = notificationService;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<bool> FollowUserAsync(Guid followerId, Guid followingId)
    {
        try
        {
            bool isFollowed = await _followRepository.AddFollowAsync(followerId, followingId);

            if (isFollowed)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                    var userDto = await userService.GetUserByIdAsync(followerId);
                    string followerName = userDto?.Name ?? followerId.ToString();

                    await _notificationService.SendNotificationAsync(
                        followingId.ToString(),
                        $"{followerName} te ha seguido.",
                        "notification");
                }

                await _notificationService.SendNotificationAsync(
                    followerId.ToString(),
                    "Follow realizado correctamente.",
                    "notification");
            }
            else
            {
                await _notificationService.SendNotificationAsync(
                    followerId.ToString(),
                    "Error al realizar follow.",
                    "notification");
            }

            return isFollowed;
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Ya sigues"))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                    var targetDto = await userService.GetUserByIdAsync(followingId);
                    string targetName = targetDto?.Name ?? followingId.ToString();

                    await _notificationService.SendNotificationAsync(
                        followerId.ToString(),
                        $"Ya estás siguiendo a {targetName}.",
                        "notification");
                }
            }
            else
            {
                await _notificationService.SendNotificationAsync(
                    followerId.ToString(),
                    ex.Message,
                    "notification");
            }

            return false;
        }
    }
}
