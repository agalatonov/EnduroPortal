using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnduroPortal.SDK
{
    public interface IUserActionsGrpcService
    {
        Task<List<Event>> GetEvents();
        Task<Event> GetEvent(int id);
        Task<IActionResult> Registration(ParticipantRegistrationDTO participantRegistrationDTO);
    }

    public class UserActionsGrpcService : IUserActionsGrpcService
    {

        public Task<Event> GetEvent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetEvents()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Registration(ParticipantRegistrationDTO participantRegistrationDTO)
        {
            throw new NotImplementedException();
        }
    }
}
