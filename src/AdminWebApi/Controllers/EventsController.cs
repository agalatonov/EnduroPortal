using Domain.Models;
using Domain.Models.DTO;
using EnduroPortal.SDK.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AdminWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventsActionGrpcService _eventActionsGrpcService;

        public EventsController(ILogger<EventsController> logger, IEventsActionGrpcService eventActionsGrpcService)
        {
            _logger = logger;
            _eventActionsGrpcService = eventActionsGrpcService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/events/event/{*slug}")]
        public async Task<IActionResult> GetEvent(string slug)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation($"AdminWebApi.Controllers.GetEvent(): request: try get event by slug: '{slug}'");
            }

            var result = await _eventActionsGrpcService.GetEvent(slug);

            if (result is null)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError($"AdminWebApi.Controllers.DeleteEvent(): Event with slug '{slug}' isn't  exist");
                }
                return BadRequest($"Event with slug {slug} is already exist");
            }
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
                _logger.LogInformation($"AdminWebApi.Controllers.GetEvents(): Processing request: get all events by the year: '{year}'");
            }

            var result = await _eventActionsGrpcService.GetEvents(year);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/events/add")]
        public async Task<IActionResult> AddEvent(AddEventDTO addEventDTO)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation($"AdminWebApi.Controllers.AddEvent(): Create new event '{addEventDTO.Name}'");
            }

            var result = await _eventActionsGrpcService.AddEvent(addEventDTO);

            if (result is null)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError($"AdminWebApi.Controllers.DeleteEvent(): Event with slug '{addEventDTO.Slug}' is already exist");
                }
                return BadRequest($"Event with slug {addEventDTO.Slug} is already exist");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("/events/delete/{*slug}")]
        public async Task<IActionResult> DeleteEvent(string slug)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation($"AdminWebApi.Controllers.DeleteEvent(): Processing request: Delete event with slug '{slug}'");
            }

            var result = await _eventActionsGrpcService.DeleteEvent(slug);
            if (string.IsNullOrEmpty(result))
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    if (_logger.IsEnabled(LogLevel.Error))
                    {
                        _logger.LogError($"AdminWebApi.Controllers.DeleteEvent(): Event with slug '{slug}' isn't exist");
                    }

                    return BadRequest($"AdminWebApi.Controllers.DeleteEvent(): Event with slug {slug} isn't exist");
                }
            }
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/events/update")]
        public async Task<IActionResult> UpdateEvent(UpdateEventDTO updateEventDTO)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation($"Processing request: Update event with slug '{updateEventDTO.Slug}'");
            }

            var result = await _eventActionsGrpcService.UpdateEvent(updateEventDTO);

            if (result is null)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError($"AdminWebApi.Controllers.DeleteEvent(): Event with slug '{updateEventDTO.Slug}' isn't exist");
                }
                return BadRequest($"Event with slug ;{updateEventDTO.Slug}' isn't exist");
            }

            return Ok(result);
        }
    }
}
