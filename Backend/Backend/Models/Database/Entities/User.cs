using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Backend.Models.Database.Enum;

namespace Backend.Models.Database.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? LastName { get; set; }

    [EmailAddress]
    public required string Mail { get; set; }

    public required string Password { get; set; }

    public  Role Role { get; set; } = Role.User;

    public string? Biography { get; set; }
    public string? AvatarUrl { get; set; }

    public string? School { get; set; }
    public string? Degree { get; set; }
    public string? Nationality { get; set; }

    public string? City { get; set; }
    public string? ErasmusCountry { get; set; }
    public DateOnly ErasmusDate {get; set;}

    [Phone]
    public required string Phone { get; set; }
    public ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
    public List<SocialMediaLink> SocialMedias { get; set; } = new List<SocialMediaLink>();
    public List<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    [InverseProperty(nameof(Event.Creator))]
    public virtual ICollection<Event> CreatedEvents { get; set; } = new List<Event>();

    [InverseProperty(nameof(Event.Participants))]
    public virtual ICollection<Event> ParticipatingEvents { get; set; } = new List<Event>();
    public virtual Hosts? Host { get; set; }

}
