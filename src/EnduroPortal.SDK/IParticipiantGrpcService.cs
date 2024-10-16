using Domain.Models;
using EnduroPortal.GrpcServer;
using EnduroPortal.SDK.Utils;

namespace EnduroPortal.SDK
{
    public interface IParticipiantGrpcService
    {
        Task<string> RemoveParticipiant(string participiantEmail);
        Task<string> AddParticipiant(AddParticipiantDTO participantRegistrationDTO);
        Task<List<Domain.Models.Entities.Participiant>> GetParticipiants(string eventSlug);
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
            var addParticipiantRequest = GrpcConversions.GetAddParticipiantRequest(participantRegistrationDTO);

            var response = await _participiantClient.AddParticipiantAsync(addParticipiantRequest);

            return response.Result;
        }

        public async Task<List<Domain.Models.Entities.Participiant>> GetParticipiants(string eventSlug)
        {
            var addParticipiantRequest = new GetParticipiantsRequest { EventSlug = eventSlug };

            var addParticipiantResponse = await _participiantClient.GetParticipiantsAsync(addParticipiantRequest);

            var response = GrpcConversions.GetParticipiants(addParticipiantResponse);

            return response;
        }

        public async Task<string> RemoveParticipiant(string participiantEmail)
        {
            var removeParticipiantRequest = new RemovePatricipianRequest { Email = participiantEmail };

            var response = await _participiantClient.RemoveParticipiantAsync(removeParticipiantRequest);

            return response.Result;
        }
    }
}
