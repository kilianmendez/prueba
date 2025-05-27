using Backend.Models.Database.Entities;
using Backend.Models.Dtos;

namespace Backend.Models.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Mail = user.Mail,
                Role = user.Role,
                Biography = user.Biography,
                AvatarUrl = user.AvatarUrl,
                School = user.School,
                City = user.City,
                Degree = user.Degree,
                Nationality = user.Nationality,
                ErasmusCountry = user.ErasmusCountry,
                Phone = user.Phone,
                ErasmusDate = (int)(DateTime.Now - user.ErasmusDate.ToDateTime(TimeOnly.MinValue)).TotalDays,
                SocialMedias = user.SocialMedias
            .Select(sm => new SocialMediaLink
            {
                SocialMedia = sm.SocialMedia,
                Url = sm.Url
            })
            .ToList()
            };
        }

        public static User ToEntity(UserDto dto, string password = "")
        {
            if (dto == null) return null;

            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                Mail = dto.Mail,
                Role = dto.Role,
                Biography = dto.Biography,
                AvatarUrl = dto.AvatarUrl,
                School = dto.School,
                City = dto.City,
                Degree = dto.Degree,
                Nationality = dto.Nationality,
                Phone = dto.Phone,
                ErasmusCountry = dto.ErasmusCountry,
                ErasmusDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-dto.ErasmusDate),
                Password = password,
                SocialMedias = dto.SocialMedias
            .Select(sm => new SocialMediaLink
            {
                SocialMedia = sm.SocialMedia,
                Url = sm.Url
            })
            .ToList()
            };
        }
    }
}
