using ManageEventBackend.Applications.DTOs.Event;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ManageEventBackend.Controllers
{
    [Route("api/event")]
    [ODataRouteComponent("event")]
    [ApiController]
    public class EventController : ODataController
    {
        private readonly IEventRepository eventRepository;
        public EventController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<EventResponse>> GetAllEvents()
        {
            var events = eventRepository.GetAllEvent();
            return Ok(events.AsQueryable());
        }

        [HttpGet("id={id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid eventId))
            {
                return BadRequest("Invalid event ID format.");
            }
            var response = await eventRepository.GetEventById(eventId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
        {
            var response = await eventRepository.CreateEvent(eventDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventDto eventDto)
        {
            var response = await eventRepository.UpdateEvent(eventDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute]string id)
        {
            if (!Guid.TryParse(id, out Guid eventId))
            {
                return BadRequest("Invalid event ID format.");
            }
            var response = await eventRepository.DeleteEvent(eventId);
            return StatusCode(response.StatusCode, response);
        }

    }
}
