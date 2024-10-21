namespace Domain.Models.DTO
{
    public class AddParticipiantDTO
    {
        public required string EventSlud { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
