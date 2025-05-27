namespace Backend.Models.Interfaces;

public interface IFollowService
{
    Task<bool> FollowUserAsync(Guid followerId, Guid followingId);
}
