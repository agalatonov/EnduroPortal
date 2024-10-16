
namespace Domain.Models.Entities
{
    public class Participiant
    {
        public required string Name { get; set; }
        public required string EventSlug { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
