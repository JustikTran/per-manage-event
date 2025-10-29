using ManageEventBackend.Applications.DTOs.Auth;
using ManageEventBackend.Applications.DTOs.User;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Security.Claims;

namespace ManageEventBackend.Controllers
{
    [Route("api/user")]
    [ODataRouteComponent("user")]
    [ApiController]
    [Authorize]
    public class UserController : ODataController
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<UserResponse>> GetAllUser()
        {
            var listUsers = userRepository.GetAllUser();
            return Ok(listUsers.AsQueryable());
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetUserByToken()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return StatusCode(401, new
                {
                    StatusCode = 401,
                    Message = "Invalid or missing user token."
                });
            }

            if (!Guid.TryParse(userId, out Guid id))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Error = "Invalid user ID format."
                });
            }

            var response = await userRepository.GetUserById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var response = await userRepository.GetUserById(userId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
        {
            var response = await userRepository.UpdateUser(userDto);
            return NoContent();
        }


        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await userRepository.DeleteUser(Guid.Parse(id));
            return StatusCode(response.StatusCode, response);
        }

    }
}
