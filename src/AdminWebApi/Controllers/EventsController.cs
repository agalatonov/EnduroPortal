using Domain.Models;
using Domain.Models.DTO;
using EnduroPortal.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventsActionGrpcService _userActionsGrpcService;

        public EventsController(ILogger<EventsController> logger, IEventsActionGrpcService userActionsGrpcService)
        {
            _logger = logger;
            _userActionsGrpcService = userActionsGrpcService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/event/{*slug}")]
        public async Task<IActionResult> GetEvent(string slug)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation($"Processing request: try get event by slug: '{slug}'");
            }

            var result = await _userActionsGrpcService.GetEvent(slug);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/events/{*year}")]
        public async Task<IActionResult> GetEvents(int year)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation($"Processing request: get all events by the year: '{year}'");
            }

            var result = await _userActionsGrpcService.GetEvents(year);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/events/addevent")]
        public async Task<IActionResult> AddEvent(AddEventDTO addEventDTO)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation($"Processing request: Create new event '{addEventDTO.Name}'");
            }

            var result = await _userActionsGrpcService.AddEvent(addEventDTO);
            return Ok(result);
        }
    }
}
