using Backend.Models.Database.Enum;

public class SocialMediasUpdateRequest
{
    public List<SocialMediaLinkDto> SocialMedias { get; set; } = new();
}

public class SocialMediaLinkDto
{
    public SocialMedia SocialMedia { get; set; }
    public string Url { get; set; }
}