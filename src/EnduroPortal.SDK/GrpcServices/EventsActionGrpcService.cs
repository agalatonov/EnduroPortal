using Domain.Models;
using Domain.Models.DTO;
using EnduroPortal.GrpcServer;
using EnduroPortal.SDK.Interfaces;
using EnduroPortal.SDK.Utils;

namespace EnduroPortal.SDK.GrpcServices
{
    public class EventsActionGrpcService : IEventsActionGrpcService
    {
        private readonly Events.EventsClient _eventsClient;

        public EventsActionGrpcService(Events.EventsClient eventsClient)
        {
            _eventsClient = eventsClient;
        }

        public async Task<Event?> GetEvent(string slug)
        {
            var grpcRequest = new GetEventRequest
            {
                Slug = slug
            };

            var getEventResponse = await _eventsClient.GetEventAsync(grpcRequest);

            if (!string.IsNullOrEmpty(getEventResponse.Result))
            {
                return null;
            }

            var result = GrpcConversions.GetEvent(getEventResponse);

            return result;
        }

        public async Task<List<Event>> GetEvents(int year)
        {
            var grpcRequest = new GetEventsRequest
            {
                Year = year
            };

            var grpcResponse = await _eventsClient.GetEventsAsync(grpcRequest);

            var result = GrpcConversions.GetEvents(grpcResponse);

            return result;
        }

        public async Task<Event?> AddEvent(AddEventDTO addEventDTO)
        {
            var request = GrpcConversions.GetAddEventRequest(addEventDTO);
            var addEventResponse = await _eventsClient.AddEventAsync(request);

            if (addEventResponse.Result != null)
            {
                return null;
            }
            var result = GrpcConversions.GetEvent(addEventResponse);
            return result;
        }
    }
}
