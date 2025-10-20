using ManageEventBackend.Applications.DTOs.Auth;
using ManageEventBackend.Applications.DTOs.User;
using ManageEventBackend.Domains.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ManageEventBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await this.authService.Login(loginDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto userDto)
        {
            var response = await this.authService.Register(userDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] LoginDto loginDto)
        {
            var response = await this.authService.ForgotPassword(loginDto);
            return StatusCode(response.StatusCode, response);
        }
    }

}
