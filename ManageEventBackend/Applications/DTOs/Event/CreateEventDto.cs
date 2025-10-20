using ManageEventBackend.Domains.Validator;
using System.ComponentModel.DataAnnotations;

namespace ManageEventBackend.Applications.DTOs.Event
{
    public class CreateEventDto
    {
        [Required]
        [IsGuid]
        public string AuthorId { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Status { get; set; } = string.Empty;

        [IsValidStart]
        [Required]
        public DateTime StartDate { get; set; }
    }
}
