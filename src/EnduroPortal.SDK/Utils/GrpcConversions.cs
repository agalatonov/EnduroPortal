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

        internal static AddParticipiantRequest GetAddParticipiantRequest(AddParticipiantDTO addParticipiantDTO)
        {
            var result = new AddParticipiantRequest
            {
                Name = addParticipiantDTO.Name,
                EventSlug = addParticipiantDTO.EventSlud,
                Email = addParticipiantDTO.Email,
                Phone = addParticipiantDTO.Phone
            };

            return result;
        }

        internal static List<Domain.Models.Entities.Participiant> GetParticipiants(GetParticipiantsResponse getParticipiantsResponse)
        {
            var result = new List<Domain.Models.Entities.Participiant>();

            foreach (var p in getParticipiantsResponse.Participiants)
            {
                result.Add(
                    new Domain.Models.Entities.Participiant
                    {
                        Name = p.Name,
                        EventSlug = p.EventSlug,
                        Email = p.Email,
                        Phone = p.Phone
                    }
                );
            }

            return result;

        }
    }
}
