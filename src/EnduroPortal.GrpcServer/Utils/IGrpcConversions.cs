using Infrastructure.Models;

namespace EnduroPortal.GrpcServer.Utils
{
    public interface IGrpcConversions
    {
        Event GetEvent(AddEventRequest addEventRequest);
        void FillGetEventResponse(Event eventdb, ref GetEventResponse response);
        void FillUpdateEventResponse(Event eventdb, ref UpdateEventResponse response);
        void UpdateEvent(ref Event @event, UpdateEventRequest addEventRequest);
        GetEventsResponse GetEventsResponse(List<Event> events);
        GetParticipiantsResponse GetParticipiantsResponse(List<Participiant> dbParticipiants);
        Participiant GetParticipiant(AddParticipiantRequest addParticipiantRequest);
        void FillAddEventResponse(Event eventdb, ref AddEventResponse response);
    }
}
