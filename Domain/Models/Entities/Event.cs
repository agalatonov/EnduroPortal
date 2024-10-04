namespace Domain.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required DateTime Date { get; set; }
        public required string Location { get; set; }
    }
}
