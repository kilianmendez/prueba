using Backend.Models.Database.Entities;
using Backend.Models.Dtos;

namespace Backend.Services
{
    public static class EventMapper
    {
        public static EventDto ToDto(Event e, bool joined) => new()
        {
            Id = e.Id,
            Title = e.Title,
            Date = e.Date,
            Location = e.Location,
            Address = e.Address,
            AttendeesCount = e.AttendeesCount,
            MaxAttendees = e.MaxAttendees,
            Category = e.Category,
            Description = e.Description,
            Organizer = e.Organizer,
            ImageUrl = e.ImageUrl,
            Tags = e.Tags.ToList(),
            
        };

        public static Event FromCreateDto(EventCreateDto dto) => new()
        {
            CreatorId = dto.CreatorId,
            Title = dto.Title,
            Date = dto.Date,
            Location = dto.Location,
            Address = dto.Address,
            MaxAttendees = dto.MaxAttendees,
            Category = dto.Category,
            Description = dto.Description,
            Tags = dto.Tags.ToList()
        };

        public static void UpdateEntity(Event e, EventUpdateDto dto)
        {
            e.Title = dto.Title;
            e.Date = dto.Date;
            e.Location = dto.Location;
            e.Address = dto.Address;
            e.MaxAttendees = dto.MaxAttendees;
            e.Category = dto.Category;
            e.Description = dto.Description;

            e.Tags.Clear();
            foreach (var t in dto.Tags) e.Tags.Add(t);
        }
    }
}
