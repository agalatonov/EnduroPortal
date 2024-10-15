using Domain.Models;
using Domain.Models.DTO;
using EnduroPortal.GrpcServer;
using Google.Protobuf.WellKnownTypes;

namespace EnduroPortal.SDK.Utils
{
    public class GrpcConversions
    {
        internal static List<Event> GetEvents(GetEventsResponse grpcResponse)
        {
            List<Event> result = new List<Event>();

            foreach (var i in grpcResponse.Events)
            {
                result.Add(GetEvent(i));
            }

            return result;
        }

        internal static Event GetEvent(GetEventResponse grpcResponse)
        {
            var result = new Event
            {
                Name = grpcResponse.Name,
                Date = grpcResponse.Date.ToDateTime(),
                Slug = grpcResponse.Slug,
                Description = grpcResponse.Description,
                Location = grpcResponse.Location
            };

            return result;
        }

        internal static Event GetEvent(AddEventResponse grpcResponse)
        {
            var result = new Event
            {
                Name = grpcResponse.Name,
                Date = grpcResponse.Date.ToDateTime(),
                Slug = grpcResponse.Slug,
                Description = grpcResponse.Description,
                Location = grpcResponse.Location
            };

            return result;
        }

        internal static AddEventRequest GetAddEventRequest(AddEventDTO addEventDTO)
        {
            var result = new AddEventRequest
            {
                Name = addEventDTO.Name,
                Slug = addEventDTO.Slug,
                Description = addEventDTO.Description,
                Location = addEventDTO.Location,
                Date = addEventDTO.Date.ToTimestamp()
            };

            return result;
        }
    }
}
