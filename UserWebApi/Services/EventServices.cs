using Domain.Models;
using EnduroPortal.SDK;
using Microsoft.AspNetCore.Mvc;

namespace UserWebApi.Services
{
    public class EventServices : IEventsService
    {
        IUserActionsGrpcService _userActionsGrpcService;

        public EventServices(IUserActionsGrpcService userActionsGrpcService)
        {
            _userActionsGrpcService = userActionsGrpcService;
        }

        public async Task<IActionResult> GetEvent(int id)
        {
            var result = await _userActionsGrpcService.GetEvent(id);

            return result
        }

        public Task<IActionResult> GetEvents()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Registartion(ParticipantRegistrationDTO participantRegistration)
        {
            throw new NotImplementedException();
        }
    }
}
