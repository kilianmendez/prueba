// Backend.Models.Mappers/AccommodationMapper.cs
using Backend.Models.Database.Entities;
using Backend.Models.Dtos;
using System.Linq;

public static class AccommodationMapper
{
    public static AccommodationDTO ToDto(Accommodation a)
    {
        return new AccommodationDTO
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            Address = a.Address,
            City = a.City,
            Country = a.Country,
            PricePerMonth = a.PricePerMonth,
            NumberOfRooms = a.NumberOfRooms,
            Bathrooms = a.Bathrooms,
            SquareMeters = a.SquareMeters,
            HasWifi = a.HasWifi,
            OwnerId = a.OwnerId,
            AvailableFrom = a.AvailableFrom,
            AvailableTo = a.AvailableTo,
            AcommodationType = a.AcommodationType,
            Images = a.AccomodationImages
                                 .Select(img => img.Url)
                                 .ToList()
        };
    }

    public static Accommodation ToEntity(AccommodationCreateDTO dto)
    {
        return new Accommodation
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Address = dto.Address,
            City = dto.City,
            Country = dto.Country,
            PricePerMonth = dto.PricePerMonth,
            NumberOfRooms = dto.NumberOfRooms,
            Bathrooms = dto.Bathrooms,
            SquareMeters = dto.SquareMeters,
            HasWifi = dto.HasWifi,
            OwnerId = dto.OwnerId,
            AvailableFrom = dto.AvailableFrom,
            AvailableTo = dto.AvailableTo,
            AcommodationType = dto.AcommodationType,

        };
    }
}
