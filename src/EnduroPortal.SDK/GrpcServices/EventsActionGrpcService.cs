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
        private readonly IGrpcConversions _grpcConversios;

        public EventsActionGrpcService(IGrpcConversions grpcConversions, Events.EventsClient eventsClient)
        {
            _eventsClient = eventsClient;
            _grpcConversios = grpcConversions;
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

            var result = _grpcConversios.GetEvent(getEventResponse);

            return result;
        }

        public async Task<List<Event>> GetEvents(int year)
        {
            var grpcRequest = new GetEventsRequest
            {
                Year = year
            };

            var grpcResponse = await _eventsClient.GetEventsAsync(grpcRequest);

            var result = _grpcConversios.GetEvents(grpcResponse);

            return result;
        }

        public async Task<Event?> AddEvent(AddEventDTO addEventDTO)
        {
            var request = _grpcConversios.GetAddEventRequest(addEventDTO);
            var addEventResponse = await _eventsClient.AddEventAsync(request);

            if (!String.IsNullOrEmpty(addEventResponse.Result))
            {
                return null;
            }
            var result = _grpcConversios.GetEvent(addEventResponse);
            return result;
        }

        public async Task<Event?> UpdateEvent(UpdateEventDTO updateEventDTO)
        {
            var request = _grpcConversios.GetUpdateEventRequest(updateEventDTO);
            var addEventResponse = await _eventsClient.UpdateEventAsync(request);

            if (!String.IsNullOrEmpty(addEventResponse.Result))
            {
                return null;
            }
            var result = _grpcConversios.GetEvent(addEventResponse);
            return result;
        }

        public async Task<string> DeleteEvent(string slug)
        {
            var request = new DeleteEventRequest { Slug = slug };

            var deleteEventResponse = await _eventsClient.DeleteEventAsync(request);

            return deleteEventResponse.Result;
        }
    }
}
