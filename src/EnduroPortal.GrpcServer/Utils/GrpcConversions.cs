using Google.Protobuf.WellKnownTypes;
using dbModels = Infrastructure.Models;

namespace EnduroPortal.GrpcServer.Utils
{
    public static class GrpcConversions
    {
        internal static dbModels.Event GetEvent(AddEventRequest addEventRequest)
        {
            var result = new dbModels.Event
            {
                Id = Guid.NewGuid(),
                Name = addEventRequest.Name,
                Description = addEventRequest.Description,
                Slug = addEventRequest.Slug,
                Date = addEventRequest.Date.ToDateTime(),
                Location = addEventRequest.Location
            };

            return result;
        }

        internal static void GetEventResponse(dbModels.Event eventdb, ref GetEventResponse response)
        {
            response.Name = eventdb.Name;
            response.Description = eventdb.Description;
            response.Slug = eventdb.Slug;
            response.Date = eventdb.Date.ToTimestamp();
            response.Location = eventdb.Location;
        }

        internal static void UpdateEventResponse(dbModels.Event eventdb, ref UpdateEventResponse response)
        {
            response.Name = eventdb.Name;
            response.Description = eventdb.Description;
            response.Slug = eventdb.Slug;
            response.Date = eventdb.Date.ToTimestamp();
            response.Location = eventdb.Location;
        }

        internal static GetEventsResponse GetEventsResponse(List<dbModels.Event> events)
        {
            var response = new GetEventsResponse();

            foreach (var e in events)
            {
                var eventResponse = new GetEventResponse();
                GetEventResponse(e, ref eventResponse);

                response.Events.Add(eventResponse);
            }

            return response;
        }

        internal static void UpdateEvent(ref dbModels.Event @event, UpdateEventRequest addEventRequest)
        {
            @event.Name = addEventRequest.Name;
            @event.Description = addEventRequest.Description;
            @event.Slug = addEventRequest.Slug;
            @event.Date = addEventRequest.Date.ToDateTime();
            @event.Location = addEventRequest.Location;
        }

        internal static dbModels.Participiant GetParticipiant(AddParticipiantRequest addParticipiantRequest)
        {
            var result = new dbModels.Participiant
            {
                Id = Guid.NewGuid(),
                Name = addParticipiantRequest.Name,
                EventSlug = addParticipiantRequest.EventSlug,
                Email = addParticipiantRequest.Email,
                Phone = addParticipiantRequest.Phone
            };

            return result;
        }

        internal static GetParticipiantsResponse GetParticipiantsResponse(List<dbModels.Participiant> dbParticipiants)
        {
            var response = new GetParticipiantsResponse();

            foreach (var p in dbParticipiants)
            {
                response.Participiants.Add(GetParticipiantDesc(p));
            }
            return response;
        }

        internal static ParticipiantDesc GetParticipiantDesc(dbModels.Participiant dbParticipiant)
        {
            var result = new ParticipiantDesc
            {
                Name = dbParticipiant.Name,
                EventSlug = dbParticipiant.EventSlug,
                Email = dbParticipiant.Email,
                Phone = dbParticipiant.Phone
            };

            return result;
        }

        internal static void GetEventResponse(dbModels.Event eventdb, ref AddEventResponse response)
        {
            response.Name = eventdb.Name;
            response.Description = eventdb.Description;
            response.Slug = eventdb.Slug;
            response.Date = eventdb.Date.ToTimestamp();
            response.Location = eventdb.Location;
        }
    }
}
