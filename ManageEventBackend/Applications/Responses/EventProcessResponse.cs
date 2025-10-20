using ManageEventBackend.Domains.Entities;

namespace ManageEventBackend.Applications.Responses
{
    public class EventProcessResponse
    {
        public string? Id { get; set; }
        public string? EventId { get; set; }
        public string? Content { get; set; }
        public DateTime StartTime { get; set; }
        public string? Status { get; set; } 
        public int ExtendedTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class EventProcessMapper:MapperPattern<EventProcessMapper>
    {
        public EventProcessResponse ToResponse(EventProcess? eventProcess)
        {
            return new()
            {
                Id = eventProcess?.Id.ToString(),
                EventId = eventProcess?.EventId.ToString(),
                Content = eventProcess?.Content,
                StartTime = eventProcess?.StartTime ?? default,
                Status = eventProcess?.Status,
                ExtendedTime = eventProcess?.ExtendedTime ?? default,
                CreatedAt = eventProcess?.CreatedAt ?? default,
                UpdatedAt = eventProcess?.UpdatedAt ?? default
            };
        }
    }
}
