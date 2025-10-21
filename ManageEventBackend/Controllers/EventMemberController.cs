using ManageEventBackend.Applications.DTOs.EventMember;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ManageEventBackend.Controllers
{
    [Route("api/participant")]
    [ODataRouteComponent("participant")]
    [ApiController]
    [Authorize]
    public class EventMemberController : ODataController
    {
        private readonly IEventMemberRepository repo;

        public EventMemberController(IEventMemberRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<EventMemberResponse>> GetAllParticipants()
        {
            var participants = repo.GetAllParticipants();
            return Ok(participants);
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetParticipantById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid pid))
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid GUID format."
                });
            }
            var response = await repo.GetParticipantById(pid);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipant([FromBody] CreateEventMemberDto memberDto)
        {
            var response = await repo.AddParticipant(memberDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateParticipant([FromBody] UpdateEventMemberDto memberDto)
        {
            if (!Guid.TryParse(memberDto.Id, out Guid _))
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid GUID format."
                });
            }
            var response = await repo.UpdateParticipant(memberDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteParticipant([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid pid))
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid GUID format."
                });
            }
            var response = await repo.DeleteParticipant(pid);
            return StatusCode(response.StatusCode, response);
        }
    }
}
