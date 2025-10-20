using ManageEventBackend.Applications.DTOs.Auth;
using ManageEventBackend.Applications.DTOs.User;
using ManageEventBackend.Applications.Responses;

namespace ManageEventBackend.Domains.Interfaces
{
    public interface IAuthService
    {
        Task<Response> Login(LoginDto loginDto);
        Task<Response> Logout(string token);
        Task<Response> Register(CreateUserDto userDto);
        Task<Response> ForgotPassword(LoginDto loginDto);
    }
}
