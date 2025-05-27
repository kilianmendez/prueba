namespace Backend.Models.Dtos;

public class AccommodationUpdateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public decimal? PricePerMonth { get; set; }
    public int? NumberOfRooms { get; set; }
    public int? Bathrooms { get; set; }
    public int? SquareMeters { get; set; }
    public bool? HasWifi { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableTo { get; set; }
}
