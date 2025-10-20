using ManageEventBackend.Domains.Validator;
using System.ComponentModel.DataAnnotations;

namespace ManageEventBackend.Applications.DTOs.EventGift
{
    public class CreateEventGiftDto
    {
        [Required]
        [IsGuid]
        public string EventId { get; set; } = string.Empty;

        [Required]
        public string Information { get; set; } = string.Empty;
    }
}
