using Domain.Models;
using EnduroPortal.SDK;
using Microsoft.AspNetCore.Mvc;

namespace UserWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParicipiantController : Controller
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IParticipiantGrpcService _participiantGrpcService;

        public ParicipiantController(ILogger<EventsController> logger, IParticipiantGrpcService participiantGrpcService) 
        {
            _logger = logger;
            _participiantGrpcService = participiantGrpcService;
        }

        [HttpPost]
        [Route("/add")]
        public async Task<IActionResult> AddParticipiant(AddParticipiantDTO participantRegistrationDTO)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation($"Processing request: participiant registration by email: '{participantRegistrationDTO.Email}'");
                }

                var result = await _participiantGrpcService.AddParticipiant(participantRegistrationDTO);

                return string.IsNullOrEmpty(result)? Ok(): BadRequest(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/delete/{*email}")]
        public async Task<IActionResult> DeleteParticipiant(string email)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation($"Processing request: delete participiant by email: '{email}'");
                }

                var result = await _participiantGrpcService.RemoveParticipiant(email);

                return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
