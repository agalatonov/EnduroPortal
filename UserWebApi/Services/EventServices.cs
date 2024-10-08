using Domain.Models;
using EnduroPortal.SDK;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace UserWebApi.Services
{
    public class EventServices : IEventServices
    {
        IUserActionsGrpcService userActionsGrpcService;

        public EventServices(IUserActionsGrpcService userGrpсService)
        {

        }

        public Task<IActionResult> GetEvent(int id)
        {
            throw new NotImplementedException();
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
