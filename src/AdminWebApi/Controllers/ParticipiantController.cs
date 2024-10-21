using Domain.Models.DTO;
using EnduroPortal.SDK.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("/add")]
        public async Task<IActionResult> AddParticipiant(AddParticipiantDTO participantRegistrationDTO)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation($"AdminWebApi.ParicipiantController.AddParticipiant(): Processing request: participiant registration by email: '{participantRegistrationDTO.Email}'");
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
                    _logger.LogInformation($"AdminWebApi.ParicipiantController.DeleteParticipiant(): Processing request: delete participiant by email: '{email}'");
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
