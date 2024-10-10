using Domain.Models;
using EnduroPortal.GrpcServer;
using EnduroPortal.SDK.Utils;

namespace EnduroPortal.SDK
{
    public interface IEventsActionGrpcService
    {
        Task<List<Event>> GetEvents(int year);
        Task<Event> GetEvent(string slug);
        Task<bool> Registration(ParticipantRegistrationDTO participantRegistrationDTO);
    }

    public class EventsActionGrpcService : IEventsActionGrpcService
    {
        private readonly Events.EventsClient _eventsClient;

        public EventsActionGrpcService(Events.EventsClient eventsClient)
        {
            _eventsClient = eventsClient;
        }

        public Task<Event> GetEvent(string slug)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetEvents(int year)
        {
            var grpcRequest = new EventsRequest
            {
                Year = year
            };

            var grpcResponse = await _eventsClient.GetEventsAsync(grpcRequest);

            var result = GrpcConversions.Convert(grpcResponse);

            return result;
        }

        public Task<bool> Registration(ParticipantRegistrationDTO participantRegistrationDTO)
        {
            throw new NotImplementedException();
        }
    }
}
