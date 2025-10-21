using ManageEventBackend.Applications.DTOs.EventGift;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ManageEventBackend.Controllers
{
    [Route("api/gift")]
    [ODataRouteComponent("gift")]
    [ApiController]
    [Authorize]
    public class EventGiftController : ODataController
    {
        private readonly IEventGiftRepository eventGiftRepository;
        public EventGiftController(IEventGiftRepository eventGiftRepository)
        {
            this.eventGiftRepository = eventGiftRepository;
        }

        [HttpGet]
        [EnableQuery]
        [AllowAnonymous]
        public ActionResult<IEnumerable<EventGiftResponse>> GetAllGifts()
        {
            var gifts = eventGiftRepository.GetAllGifts();
            return Ok(gifts.AsQueryable());
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid giftId))
            {
                return BadRequest("Invalid gift ID format.");
            }
            var response = await eventGiftRepository.GetById(giftId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGift([FromBody] CreateEventGiftDto giftDto)
        {
            var response = await eventGiftRepository.CreateGift(giftDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGift([FromBody] UpdateEventGiftDto giftDto)
        {
            var response = await eventGiftRepository.UpdateGift(giftDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteGift([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid giftId))
            {
                return BadRequest("Invalid gift ID format.");
            }
            var response = await eventGiftRepository.DeleteGift(giftId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
