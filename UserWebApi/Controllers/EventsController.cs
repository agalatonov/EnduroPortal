using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UserWebApi.Services;

namespace UserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventsService _eventsService;

        public EventsController(ILogger<EventsController> logger, IEventsService eventsService)
        {
            _logger = logger;
            _eventsService = eventsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("GetEvent/{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            var result = await _eventsService.GetEvent(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var result = await _eventsService.GetEvents();
            return Ok(result);
        }


    }
}
