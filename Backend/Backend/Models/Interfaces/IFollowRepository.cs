namespace Backend.Models.Interfaces;

public interface IFollowRepository
{
    Task<bool> AddFollowAsync(Guid followerId, Guid followingId);
}

