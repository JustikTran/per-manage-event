using BCrypt.Net;
using ManageEventBackend.Applications.DTOs.Auth;
using ManageEventBackend.Applications.DTOs.User;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Applications.Services;
using ManageEventBackend.Domains.Entities;
using ManageEventBackend.Domains.Interfaces;
using ManageEventBackend.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

namespace ManageEventBackend.Infrastructures.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        private readonly TokenService tokenService;
        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.tokenService = new TokenService(configuration);
        }
        public async Task<Response> CreateUser(CreateUserDto createUserDTO)
        {
            try
            {
                User newUser = new()
                {
                    Username = createUserDTO.Username,
                    Email = createUserDTO.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password),
                    Avatar = createUserDTO.Avatar,
                    FirstName = createUserDTO.FirstName,
                    LastName = createUserDTO.LastName,
                    Role = createUserDTO.Role,
                    IsDelete = false,
                    RefeshToken = string.Empty,
                };

                context.Users.Add(newUser);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "User created successfully"
                };
            }
            catch (Exception err)
            {
                throw new BadHttpRequestException("An occurred error while creating user.", 500, err);
            }
        }

        public async Task<Response> DeleteUser(Guid id)
        {
            try
            {
                var existingUser = await context.Users.FindAsync(id);
                if (existingUser == null || existingUser.IsDelete)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                existingUser.IsDelete = true;
                context.Users.Update(existingUser);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "User deleted successfully"
                };
            }
            catch (Exception err)
            {
                throw new BadHttpRequestException("An occurred error while delete user.", 500, err);
            }
        }

        public async Task<Response> ForgotPassword(LoginDto loginDto)
        {
            try
            {
                var existing = await context.Users
                    .FirstOrDefaultAsync(u => (u.Username == loginDto.Username || u.Email == loginDto.Username) && !u.IsDelete);
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }

                existing.Password = BCrypt.Net.BCrypt.HashPassword(loginDto.Password);
                existing.UpdatedAt = DateTime.UtcNow;

                context.Users.Update(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Password reset successfully"
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while change user's password.", 500, err);
            }
        }

        public IQueryable<UserResponse> GetAllUser()
        {
            try
            {
                var listUsers = context.Users.Where(u => !u.IsDelete).ToList();
                return listUsers.AsQueryable().Select(user => UserMapper.Instance.ToResponse(user));
            }
            catch (Exception err)
            {
                throw new BadHttpRequestException("An occurred error while get all users.", 500, err);
            }
        }

        public async Task<Response> GetUserById(Guid id)
        {
            try
            {
                var existingUser = await context.Users.FirstOrDefaultAsync(u => !u.IsDelete);
                if (existingUser == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }

                return new Response
                {
                    StatusCode = 200,
                    Data = UserMapper.Instance.ToResponse(existingUser)
                };
            }
            catch (Exception err)
            {
                throw new BadHttpRequestException("An occurred error while get a user.", 500, err);
            }
        }

        public async Task<Response> Login(LoginDto loginDto)
        {
            try
            {
                var user = await context.Users.Where(u => (u.Username == loginDto.Username || u.Email == loginDto.Username)
                        && !u.IsDelete)
                    .FirstOrDefaultAsync();

                if (user == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account does not exist."
                    };

                bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
                if (!isValidPassword)
                    return new Response
                    {
                        StatusCode = 401,
                        Message = "Invalid credentials."
                    };

                var token = tokenService.GetToken(user);
                return new Response
                {
                    StatusCode = 200,
                    Data = new
                    {
                        Token = token
                    }
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while login.", 500, err);
            }
        }

        public Task<Response> Logout(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> ResetPassword(ChangePasswordDto changePasswordDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> UpdateUser(UpdateUserDto updateUserDTO)
        {
            try
            {
                if (!Guid.TryParse(updateUserDTO.Id, out Guid userId))
                {
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Invalid user ID format."
                    };
                }
                var existingUser = await context.Users.FindAsync(userId);
                if (existingUser == null || existingUser.IsDelete)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                existingUser.FirstName = updateUserDTO.FirstName;
                existingUser.LastName = updateUserDTO.LastName;
                existingUser.Avatar = updateUserDTO.Avatar;
                existingUser.UpdatedAt = DateTime.UtcNow;
                context.Users.Update(existingUser);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "User updated successfully"
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while update user.", 500, err);
            }
        }
    }
}
