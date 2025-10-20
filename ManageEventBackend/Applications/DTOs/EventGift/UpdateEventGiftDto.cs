using ManageEventBackend.Domains.Validator;
using System.ComponentModel.DataAnnotations;

namespace ManageEventBackend.Applications.DTOs.EventGift
{
    public class UpdateEventGiftDto : CreateEventGiftDto
    {
        [Required]
        [IsGuid]
        public string Id { get; set; } = string.Empty;
    }
}
