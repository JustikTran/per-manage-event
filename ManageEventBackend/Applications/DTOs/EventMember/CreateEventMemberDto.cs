using System.ComponentModel.DataAnnotations;
using ManageEventBackend.Domains.Validator;

namespace ManageEventBackend.Applications.DTOs.EventMember
{
    public class CreateEventMemberDto
    {
        [Required]
        [IsGuid]
        public string EventId { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = default!;

        [StringLength(50)]
        public string? Nickname { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(15)]
        [Phone]
        public string? Phone { get; set; }
    }
}
