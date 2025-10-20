using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ManageEventBackend.Domains.Validator;

namespace ManageEventBackend.Applications.DTOs.EventProcess
{
    public class CreateEventProcessDto
    {
        [Required]
        [IsGuid]
        public string EventId { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        [IsValidStart]
        public DateTime StartTime { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Range(-1, int.MaxValue)]
        public int ExtendedTime { get; set; }
    }
}
