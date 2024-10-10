using Domain.Models;
using EnduroPortal.SDK;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace UserWebApi.Controllers
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
            var result = await _userActionsGrpcService.GetEvent(slug);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/events/{*year}")]
        public async Task<IActionResult> GetEvents(int year)
        {
            var result = await _userActionsGrpcService.GetEvents(year);
            return Ok(result);
        }


    }
}
