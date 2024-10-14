using Domain.Models;
using EnduroPortal.GrpcServer;

namespace EnduroPortal.SDK
{
    public interface IParticipiantGrpcService
    {
        Task<string> DeleteParticipiant(string participiantEmail);
        Task<string> AddParticipiant(ParticipantRegistrationDTO participantRegistrationDTO);
    }

    public class ParticipiantGrpcService: IParticipiantGrpcService
    {
        private readonly Events.EventsClient _eventsClient;
        public ParticipiantGrpcService(Events.EventsClient eventsClient) 
        {
            _eventsClient = eventsClient;
        }

        public async Task<string> AddParticipiant(ParticipantRegistrationDTO participantRegistrationDTO)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteParticipiant(string participiantEmail)
        {
            throw new NotImplementedException();
        }
    }
}
