using Backend.Models.Database.Entities;
using Backend.Models.Dtos;
using Backend.Models.Database.Enum;

namespace Backend.Models.Mappers;

public static class RecommendationMapper
{
    public static RecommendationDto ToDto(Recommendation recommendation)
    {
        if (recommendation == null) return null;

        return new RecommendationDto
        {
            Id = recommendation.Id,
            Title = recommendation.Title,
            Description = recommendation.Description,
            Category = recommendation.Category,
            Address = recommendation.Address,
            City = recommendation.City,
            Country = recommendation.Country,
            Rating = recommendation.Rating,
            CreatedAt = recommendation.CreatedAt,
            Tags = recommendation.Tags,
            RecommendationImages = recommendation.RecommendationImages,
            UserId = recommendation.UserId ?? Guid.Empty
        };
    }
    public static Recommendation ToEntity(RecommendationCreateRequest request)
    {
        return new Recommendation
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            Address = request.Address,
            City = request.City,
            Country = request.Country,
            Rating = request.Rating,
            CreatedAt = DateTime.UtcNow,
            Tags = request.Tags,
            UserId = request.UserId
        };
    }

    public static void UpdateEntity(Recommendation recommendation, RecommendationUpdateRequest request)
    {
        if (request.Title != null)
            recommendation.Title = request.Title;
        if (request.Description != null)
            recommendation.Description = request.Description;
        if (request.Category != null)
            recommendation.Category = request.Category;
        if (request.Address != null)
            recommendation.Address = request.Address;
        if (request.City != null)
            recommendation.City = request.City;
        if (request.Country != null)
            recommendation.Country = request.Country;
        if (request.Rating.HasValue)
            recommendation.Rating = request.Rating;
    }
}
