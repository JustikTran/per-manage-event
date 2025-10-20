using ManageEventBackend.Applications.DTOs.Auth;
using ManageEventBackend.Applications.DTOs.User;
using ManageEventBackend.Applications.Responses;

namespace ManageEventBackend.Domains.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<UserResponse> GetAllUser();
        Task<Response> GetUserById(Guid id);
        Task<Response> CreateUser(CreateUserDto createUserDTO);
        Task<Response> UpdateUser(UpdateUserDto updateUserDTO);
        Task<Response> DeleteUser(Guid id);
        Task<Response> Login(LoginDto loginDto);
        Task<Response> Logout(Guid userId);
        Task<Response> ForgotPassword(LoginDto loginDto);
        Task<Response> ResetPassword(ChangePasswordDto changePasswordDto);
    }
}
