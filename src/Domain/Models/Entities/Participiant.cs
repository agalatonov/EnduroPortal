
namespace Domain.Models.Entities
{
    internal class Participiant
    {
        public int Id { get; set; }
        public required string EventSlug { get; set; }
        public required string ParticipiantName { get; set; }
        public required string Email { get; set; }
    }
}
