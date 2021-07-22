using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSItemIndex.API.Models;
using OSItemIndex.Data;
using OSItemIndex.Data.Services;

namespace OSItemIndex.API.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet(Name = "GetEvents")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents([FromQuery] EventQuery query)
        {
            return Json(await _eventService.GetAllEventsAsync(query.Source, query.Type));
        }

        [HttpGet("recent", Name = "GetMostRecentEvent")]
        [Produces("application/json")]
        public async Task<ActionResult<Event>> GetMostRecent([FromQuery] EventQuery query)
        {
            return Json(await _eventService.GetMostRecentAsync(query.Source, query.Type));
        }
    }
}
