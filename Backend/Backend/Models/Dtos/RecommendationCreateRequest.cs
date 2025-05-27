using System.ComponentModel.DataAnnotations;
using Backend.Models.Database.Enum;

namespace Backend.Models.Dtos
{
    public class RecommendationCreateRequest
    {
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }
        public Category? Category { get; set; }
        public ICollection<String> Tags { get; set; } = new List<String>();

        public string Address { get; set; }
        public string City { get; set; }

        public Guid UserId { get; set; }
        public string Country { get; set; }
        public Rating? Rating { get; set; }
        public List<IFormFile>? Files { get; set; }


    }
}
