using ManageEventBackend.Domains.Entities;

namespace ManageEventBackend.Applications.Responses
{
    public class UserResponse
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
        public string? Avatar { get; set; }
        public string? RefeshToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UserMapper : MapperPattern<UserMapper>
    {
        public UserResponse ToResponse(User? user)
        {
            if (user == null)
            {
                return new();
            }
            return new()
            {
                Id = user.Id.ToString(),
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Avatar = user.Avatar,
                RefeshToken = user.RefeshToken,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
