using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Models.Database.Enum;

namespace Backend.Models.Database.Entities
{
    public class SocialMediaLink
    {
        [Key]
        public int Id { get; set; }
        public SocialMedia SocialMedia { get; set; }

        public string Url { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
