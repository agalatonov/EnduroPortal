namespace Domain.Models
{
    public class ParticipantRegistrationDTO
    {
        public required int EventId;
        public required string FirstName;
        public string? LastName;
        public required string Email;
    }
}
