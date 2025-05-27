using Backend.Models.Database.Enum;

namespace Backend.Models.Dtos;

public class ForumDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public ForumCategory Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatorId { get; set; }
    public string CreatorName { get; set; } = string.Empty;
    public string CreatorNationatility { get; set; } = string.Empty;
    public string CreatorAvatar { get; set; } = string.Empty;
}

public class CreateForumDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public ForumCategory Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatorId { get; set; }
}

public class CreateForumThreadDTO
{
    public Guid ForumId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CreatedBy { get; set; }
}

public class ForumThreadDTO
{
    public Guid Id { get; set; }
    public Guid ForumId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid CreatorId { get; set; }
    public string CreatorName { get; set; } = string.Empty;
    public string CreatorNationatility { get; set; } = string.Empty;
    public string CreatorAvatar { get; set; } = string.Empty;
}

public class CreateForumMessageDTO
{
    public Guid ThreadId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid CreatedBy { get; set; }
    public Guid? ParentMessageId { get; set; }
}

public class ForumMessageDTO
{
    public Guid Id { get; set; }
    public Guid ThreadId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid CreatorId { get; set; }
    public string CreatorName { get; set; } = string.Empty;
    public string CreatorNationatility { get; set; } = string.Empty;
    public string CreatorAvatar { get; set; } = string.Empty;
    public Guid? ParentMessageId { get; set; }
}
