using Backend.Models.Database.Entities;
using Backend.Models.Database.Enum;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Dtos
{
    public class UserDto
    {
        [Key]
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string LastName { get; set; }

        [EmailAddress]
        public required string Mail { get; set; }


        public required Role Role { get; set; } = Role.User;

        public string Biography { get; set; }
        public string AvatarUrl { get; set; }

        public string School { get; set; }
        public string? City { get; set; }
        public string Degree { get; set; }
        public string Nationality { get; set; }
        public int ErasmusDate { get; set; }
        public string? ErasmusCountry { get; set; }

        [Phone]
        public string Phone { get; set; }

        public List<SocialMediaLink> SocialMedias { get; set; } = new List<SocialMediaLink>();
        public string Password { get; internal set; }
    }
}
