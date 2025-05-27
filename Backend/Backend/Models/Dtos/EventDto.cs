namespace Backend.Models.Dtos
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public int AttendeesCount { get; set; }
        public int? MaxAttendees { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Organizer { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Tags { get; set; }
    }
}
