using ManageEventBackend.Applications.DTOs.EventProcess;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.RateLimiting;

namespace ManageEventBackend.Controllers
{
    [Route("api/process")]
    [ODataRouteComponent("process")]
    [ApiController]
    [Authorize]
    public class EventProcessController : ODataController
    {
        private readonly IEventProcessRepository repo;
        public EventProcessController(IEventProcessRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [EnableQuery]
        [AllowAnonymous]
        public ActionResult<IEnumerable<EventProcessResponse>> GetAllProcesses()
        {
            var processes = repo.GetAllProcesses();
            return Ok(processes.AsQueryable());
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetProcessById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid pid))
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid GUID format."
                });
            }
            var response = await repo.GetProcessById(pid);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProcess([FromBody] CreateEventProcessDto processDto)
        {
            var response = await repo.CreateProcess(processDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProcess([FromBody] UpdateEventProcessDto processDto)
        {
            if (!Guid.TryParse(processDto.Id, out Guid _))
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid GUID format."
                });
            }
            var response = await repo.UpdateProcess(processDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteProcess([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid pid))
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid GUID format."
                });
            }
            var response = await repo.DeleteProcess(pid);
            return StatusCode(response.StatusCode, response);
        }
    }
}
