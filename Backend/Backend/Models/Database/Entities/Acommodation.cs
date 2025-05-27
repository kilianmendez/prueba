using Backend.Models.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Backend.Models.Database.Enum;

public class Accommodation
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; }
    public string Description { get; set; }

    [Required]
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public decimal PricePerMonth { get; set; }
    public int NumberOfRooms { get; set; }
    public int Bathrooms { get; set; }
    public int SquareMeters { get; set; }
    public bool HasWifi { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableTo { get; set; }

    public Guid OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public User Owner { get; set; }
    public AcommodationType AcommodationType { get; set; }
    public ICollection<ImageAccommodation> AccomodationImages { get; set; } = new List<ImageAccommodation>();
}
