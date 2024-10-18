using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Participiant
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string EventSlug { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
