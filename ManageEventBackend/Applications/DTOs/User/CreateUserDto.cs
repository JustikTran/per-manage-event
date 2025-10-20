using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ManageEventBackend.Applications.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(32, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$",
            ErrorMessage = "Password must be 8-32 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DefaultValue("user")]
        public string Role { get; set; } = string.Empty;

        [Required]
        [Url]
        public string Avatar { get; set; } = default!;
    }
}
