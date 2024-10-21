using Domain.Models.DTO;
using Domain.Models.Entities;
using EnduroPortal.GrpcServer;
using EnduroPortal.SDK.Interfaces;
using EnduroPortal.SDK.Utils;

namespace EnduroPortal.SDK.GrpcServices
{
    public class ParticipiantGrpcService : IParticipiantGrpcService
    {
        private readonly Participiants.ParticipiantsClient _participiantClient;
        private readonly IGrpcConversions _grpcConversions;

        public ParticipiantGrpcService(Participiants.ParticipiantsClient participiantClient, IGrpcConversions grpcConversions)
        {
            _participiantClient = participiantClient;
            _grpcConversions = grpcConversions;
        }

        public async Task<string> AddParticipiant(AddParticipiantDTO participantRegistrationDTO)
        {
            var addParticipiantRequest = _grpcConversions.GetAddParticipiantRequest(participantRegistrationDTO);

            var response = await _participiantClient.AddParticipiantAsync(addParticipiantRequest);

            return response.Result;
        }

        public async Task<List<Participiant>> GetParticipiants(string eventSlug)
        {
            var addParticipiantRequest = new GetParticipiantsRequest { EventSlug = eventSlug };

            var addParticipiantResponse = await _participiantClient.GetParticipiantsAsync(addParticipiantRequest);

            var response = _grpcConversions.GetParticipiants(addParticipiantResponse);

            return response;
        }

        public async Task<string> RemoveParticipiant(string eventSlug, string participiantEmail)
        {
            var removeParticipiantRequest = new RemovePatricipianRequest { Email = participiantEmail };

            var response = await _participiantClient.RemoveParticipiantAsync(removeParticipiantRequest);

            return response.Result;
        }
    }
}
