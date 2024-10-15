using Domain.Models;
using EnduroPortal.GrpcServer;

namespace EnduroPortal.SDK
{
    public interface IParticipiantGrpcService
    {
        Task<string> DeleteParticipiant(string participiantEmail);
        Task<string> AddParticipiant(AddParticipiantDTO participantRegistrationDTO);
    }

    public class ParticipiantGrpcService : IParticipiantGrpcService
    {
        private readonly Participiant.ParticipiantClient _participiantClient;
        public ParticipiantGrpcService(Participiant.ParticipiantClient participiantClient)
        {
            _participiantClient = participiantClient;
        }

        public async Task<string> AddParticipiant(AddParticipiantDTO participantRegistrationDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteParticipiant(string participiantEmail)
        {
            throw new NotImplementedException();
        }
    }
}
