
namespace Infrastructure.Models
{
    public class Event
    {
        public string Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public required DateTime Date { get; set; }
        public required string Location { get; set; }
    }
}
