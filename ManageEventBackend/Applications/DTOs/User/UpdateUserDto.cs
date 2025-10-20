using ManageEventBackend.Domains.Validator;
using System.ComponentModel.DataAnnotations;

namespace ManageEventBackend.Applications.DTOs.User
{
    public class UpdateUserDto : CreateUserDto
    {
        [Required]
        [IsGuid]
        public string Id { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }
    }
}
