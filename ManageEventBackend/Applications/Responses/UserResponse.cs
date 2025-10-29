using ManageEventBackend.Domains.Entities;
using System.Text.Json.Serialization;

namespace ManageEventBackend.Applications.Responses
{
    public class UserResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("username")]
        public string? Username { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("role")]
        public string? Role { get; set; }
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")]
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
                RefreshToken = user.RefreshToken,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
