using Domain.Models;
using Domain.Models.DTO;
using Domain.Models.Entities;
using EnduroPortal.GrpcServer;
using Google.Protobuf.WellKnownTypes;

namespace EnduroPortal.SDK.Utils
{
    public class GrpcConversions : IGrpcConversions
    {
        public List<Event> GetEvents(GetEventsResponse grpcResponse)
        {
            List<Event> result = new List<Event>();

            foreach (var i in grpcResponse.Events)
            {
                result.Add(GetEvent(i));
            }

            return result;
        }

        public Event GetEvent(GetEventResponse grpcResponse)
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

        public Event GetEvent(AddEventResponse grpcResponse)
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

        public Event GetEvent(UpdateEventResponse grpcResponse)
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


        public AddEventRequest GetAddEventRequest(AddEventDTO addEventDTO)
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

        public AddParticipiantRequest GetAddParticipiantRequest(AddParticipiantDTO addParticipiantDTO)
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

        public List<Participiant> GetParticipiants(GetParticipiantsResponse getParticipiantsResponse)
        {
            var result = new List<Participiant>();

            foreach (var p in getParticipiantsResponse.Participiants)
            {
                result.Add(
                    new Participiant
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


        public UpdateEventRequest GetUpdateEventRequest(UpdateEventDTO addEventDTO)
        {
            var result = new UpdateEventRequest
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
