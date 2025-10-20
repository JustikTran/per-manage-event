using ManageEventBackend.Domains.Validator;
using System.ComponentModel.DataAnnotations;

namespace ManageEventBackend.Applications.DTOs.EventMember
{
    public class UpdateEventMemberDto : CreateEventMemberDto
    {
        [Required]
        [IsGuid]
        public string Id { get; set; } = string.Empty;
    }
}
