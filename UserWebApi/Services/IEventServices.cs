using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace UserWebApi.Services
{
    public interface IEventServices
    {
        Task<IActionResult> GetEvents();
        Task<IActionResult> GetEvent(int id);
        Task<IActionResult> Registartion(ParticipantRegistrationDTO participantRegistration);
    }
}
