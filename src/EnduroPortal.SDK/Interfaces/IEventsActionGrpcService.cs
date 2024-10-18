using Domain.Models;
using Domain.Models.DTO;

namespace EnduroPortal.SDK.Interfaces
{
    public interface IEventsActionGrpcService
    {
        Task<List<Event>> GetEvents(int year);
        Task<Event?> GetEvent(string slug);
        Task<Event?> AddEvent(AddEventDTO addEventDTO);
    }
}
