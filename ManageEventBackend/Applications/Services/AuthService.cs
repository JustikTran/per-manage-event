using ManageEventBackend.Applications.DTOs.Auth;
using ManageEventBackend.Applications.DTOs.User;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Interfaces;

namespace ManageEventBackend.Applications.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<Response> ForgotPassword(LoginDto loginDto)
        {
            try
            {
                var response = await this.userRepository.ForgotPassword(loginDto);
                return response;
            }
            catch (Exception err)
            {
                throw new BadHttpRequestException("An occurred error while change password of user.", 500, err);
            }
        }

        public async Task<Response> Login(LoginDto loginDto)
        {
            try
            {
                var response = await this.userRepository.Login(loginDto);
                return response;
            }
            catch (Exception err)
            {
                throw new BadHttpRequestException("An occurred error while login user.", 500, err);
            }
        }

        public Task<Response> Logout(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Register(CreateUserDto userDto)
        {
            try
            {
                var response = await this.userRepository.CreateUser(userDto);
                return response;
            }
            catch (Exception err)
            {
                throw new BadHttpRequestException("An occurred error while create user.", 500, err);
            }
        }
    }
}
