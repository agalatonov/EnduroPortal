using Google.Protobuf.WellKnownTypes;
using Infrastructure.Models;

namespace EnduroPortal.GrpcServer.Utils
{
    public class GrpcConversions : IGrpcConversions
    {
        public Event GetEvent(AddEventRequest addEventRequest)
        {
            var result = new Event
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

        public void FillGetEventResponse(Event eventdb, ref GetEventResponse response)
        {
            response.Name = eventdb.Name;
            response.Description = eventdb.Description;
            response.Slug = eventdb.Slug;
            response.Date = eventdb.Date.ToTimestamp();
            response.Location = eventdb.Location;
        }

        public void FillUpdateEventResponse(Event eventdb, ref UpdateEventResponse response)
        {
            response.Name = eventdb.Name;
            response.Description = eventdb.Description;
            response.Slug = eventdb.Slug;
            response.Date = eventdb.Date.ToTimestamp();
            response.Location = eventdb.Location;
        }

        public GetEventsResponse GetEventsResponse(List<Event> events)
        {
            var response = new GetEventsResponse();

            foreach (var e in events)
            {
                var eventResponse = new GetEventResponse();
                FillGetEventResponse(e, ref eventResponse);

                response.Events.Add(eventResponse);
            }

            return response;
        }

        public void UpdateEvent(ref Event @event, UpdateEventRequest addEventRequest)
        {
            @event.Name = addEventRequest.Name;
            @event.Description = addEventRequest.Description;
            @event.Slug = addEventRequest.Slug;
            @event.Date = addEventRequest.Date.ToDateTime();
            @event.Location = addEventRequest.Location;
        }

        public Participiant GetParticipiant(AddParticipiantRequest addParticipiantRequest)
        {
            var result = new Participiant
            {
                Id = Guid.NewGuid(),
                Name = addParticipiantRequest.Name,
                EventSlug = addParticipiantRequest.EventSlug,
                Email = addParticipiantRequest.Email,
                Phone = addParticipiantRequest.Phone
            };

            return result;
        }

        public GetParticipiantsResponse GetParticipiantsResponse(List<Participiant> dbParticipiants)
        {
            var response = new GetParticipiantsResponse();

            foreach (var p in dbParticipiants)
            {
                response.Participiants.Add(GetParticipiantDesc(p));
            }
            return response;
        }

        public ParticipiantDesc GetParticipiantDesc(Participiant dbParticipiant)
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

        public void FillAddEventResponse(Event eventdb, ref AddEventResponse response)
        {
            response.Name = eventdb.Name;
            response.Description = eventdb.Description;
            response.Slug = eventdb.Slug;
            response.Date = eventdb.Date.ToTimestamp();
            response.Location = eventdb.Location;
        }
    }
}
