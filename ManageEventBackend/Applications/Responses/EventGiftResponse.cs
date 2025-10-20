using ManageEventBackend.Domains.Entities;

namespace ManageEventBackend.Applications.Responses
{
    public class EventGiftResponse
    {
        public string? Id { get; set; }
        public string? EventId { get; set; }
        public string Information { get; set; } = default!;
    }

    public class EventGiftMapper:MapperPattern<EventGiftMapper>
    {
        public EventGiftResponse ToResponse(EventGift? eventGift)
        {
            return new EventGiftResponse
            {
                Id = eventGift?.Id.ToString(),
                EventId = eventGift?.EventId.ToString(),
                Information = eventGift?.Information ?? string.Empty
            };
        }
    }
}
