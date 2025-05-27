using Backend.Models.Database.Entities;
using Backend.Models.Database.Enum;

namespace Backend.Models.Dtos
{
    public class RecommendationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Rating? Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public ICollection<String> Tags { get; set; } = new List<String>();
        public ICollection<Image> RecommendationImages { get; set; } = new List<Image>();


    }
}
