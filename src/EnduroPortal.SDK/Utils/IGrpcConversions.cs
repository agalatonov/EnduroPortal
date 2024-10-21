using Domain.Models;
using Domain.Models.DTO;
using Domain.Models.Entities;
using EnduroPortal.GrpcServer;

namespace EnduroPortal.SDK.Utils
{
    public interface IGrpcConversions
    {
        List<Event> GetEvents(GetEventsResponse grpcResponse);
        Event GetEvent(GetEventResponse grpcResponse);
        Event GetEvent(AddEventResponse grpcResponse);
        Event GetEvent(UpdateEventResponse grpcResponse);
        AddEventRequest GetAddEventRequest(AddEventDTO addEventDTO);
        AddParticipiantRequest GetAddParticipiantRequest(AddParticipiantDTO addParticipiantDTO);
        List<Participiant> GetParticipiants(GetParticipiantsResponse getParticipiantsResponse);
        UpdateEventRequest GetUpdateEventRequest(UpdateEventDTO addEventDTO);
    }
}
