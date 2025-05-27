using Backend.Models.Database.Entities;
using Backend.Models.Dtos;

namespace Backend.Models.Mappers;

public class ForumMapper
{

    public static Forum ToEntity(CreateForumDTO dto)
    {
        if (dto == null) return null!;

        return new Forum
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Country = dto.Country,
            Category = dto.Category,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatorId
        };
    }

    public static ForumDTO ToDto(Forum forum)
    {
        if (forum == null) return null!;

        return new ForumDTO
        {
            Id = forum.Id,
            Title = forum.Title,
            Description = forum.Description,
            Country = forum.Country,
            Category = forum.Category,
            CreatedAt = forum.CreatedAt,
            CreatorId = forum.CreatedBy,
            CreatorName = "Nombre del Usuario",
            CreatorAvatar = "URL del Avatar"
        };
    }

    public static ForumThread ToEntity(CreateForumThreadDTO dto)
    {
        if (dto == null) return null!;

        return new ForumThread
        {
            Id = Guid.NewGuid(),
            ForumId = dto.ForumId,
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy

        };
    }

    public static ForumThreadDTO ToDto(ForumThread thread)
    {
        if (thread == null) return null!;

        return new ForumThreadDTO
        {
            Id = thread.Id,
            ForumId = thread.ForumId,
            Title = thread.Title,
            Content = thread.Content,
            CreatedAt = thread.CreatedAt,
            CreatorId = thread.CreatedBy,
            CreatorName = string.Empty,
            CreatorAvatar = string.Empty,
            CreatorNationatility = string.Empty
        };
    }

    public static ForumMessages ToEntity(CreateForumMessageDTO dto)
    {
        if (dto == null) return null!;

        return new ForumMessages
        {
            Id = Guid.NewGuid(),
            ThreadId = dto.ThreadId,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = dto.CreatedBy,
            ParentMessageId = dto.ParentMessageId
        };
    }

    public static ForumMessageDTO ToDto(ForumMessages message)
    {
        if (message == null) return null!;

        return new ForumMessageDTO
        {
            Id = message.Id,
            ThreadId = message.ThreadId,
            Content = message.Content,
            CreatedAt = message.CreatedAt,
            CreatorId = message.CreatedBy,
            ParentMessageId = message.ParentMessageId,
            CreatorName = string.Empty,
            CreatorAvatar = string.Empty,
            CreatorNationatility = string.Empty
        };
    }
}

