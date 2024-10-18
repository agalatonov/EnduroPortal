using Domain.Models;

namespace EnduroPortal.SDK.Interfaces
{
    public interface IParticipiantGrpcService
    {
        Task<string> RemoveParticipiant(string participiantEmail);
        Task<string> AddParticipiant(AddParticipiantDTO participantRegistrationDTO);
        Task<List<Domain.Models.Entities.Participiant>> GetParticipiants(string eventSlug);
    }
}
