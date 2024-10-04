using Microsoft.AspNetCore.Mvc;

namespace UserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private ILogger<EventsController> _logger;
        public EventsController(ILogger<EventsController> logger)
        {
            _logger = logger;
        }

        [Route("events")]
        public Task<IActionResult> GetEvents()
        {
            var events =
        }
    }
}
