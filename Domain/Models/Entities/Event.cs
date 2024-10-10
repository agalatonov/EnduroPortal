namespace Domain.Models
{
    public class Event
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
        public required DateTime Date { get; set; }
        public required string Location { get; set; }
    }
}
