using ManageEventBackend.Domains.Validator;
using System.ComponentModel.DataAnnotations;

namespace ManageEventBackend.Applications.DTOs.Event
{
    public class UpdateEventDto : CreateEventDto
    {
        [Required]
        [IsGuid]
        public string Id { get; set; } = string.Empty;
    }
}
