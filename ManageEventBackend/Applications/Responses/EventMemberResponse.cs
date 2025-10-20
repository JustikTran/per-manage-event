using ManageEventBackend.Domains.Entities;

namespace ManageEventBackend.Applications.Responses
{
    public class EventMemberResponse
    {
        public string? Id { get; set; }
        public string? EventId { get; set; }
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class EventMemberMapper:MapperPattern<EventMemberMapper>
    {
        public EventMemberResponse ToResponse(EventMember? eventMember)
        {
            return new()
            {
                Id = eventMember?.Id.ToString(),
                EventId = eventMember?.EventId.ToString(),
                Name = eventMember?.Name,
                Nickname = eventMember?.Nickname,
                Email = eventMember?.Email,
                Phone = eventMember?.Phone
            };
        }
    }
}
