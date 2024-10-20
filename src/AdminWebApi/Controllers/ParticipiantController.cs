using Domain.Models;
using EnduroPortal.SDK.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParicipiantController : Controller
    {
        private readonly ILogger<ParicipiantController> _logger;
        private readonly IParticipiantGrpcService _participiantGrpcService;

        public ParicipiantController(ILogger<ParicipiantController> logger, IParticipiantGrpcService participiantGrpcService)
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

                return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/delete/{eventSlug}/{email}")]
        public async Task<IActionResult> DeleteParticipiant(string eventSlug, string email)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation($"Processing request: delete participiant by email: '{email}'");
                }

                var result = await _participiantGrpcService.RemoveParticipiant(eventSlug, email);

                return string.IsNullOrEmpty(result) ? Ok() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
