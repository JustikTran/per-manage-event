using ManageEventBackend.Domains.Validator;
using System.ComponentModel.DataAnnotations;

namespace ManageEventBackend.Applications.DTOs.EventProcess
{
    public class UpdateEventProcessDto : CreateEventProcessDto
    {
        [Required]
        [IsGuid]
        public string Id { get; set; } = string.Empty;
    }
}
