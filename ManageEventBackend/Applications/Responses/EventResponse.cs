using ManageEventBackend.Domains.Entities;

namespace ManageEventBackend.Applications.Responses
{
    public class EventResponse
    {
        public string? Id { get; set; }
        public string? AuthorId { get; set; } 
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public string? Status { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class EventMapper:MapperPattern<EventMapper>
    {
        public EventResponse ToResponse(Event? even)
        {
            if (even == null)
            {
                return new();
            }
            return new()
            {
                Id = even.Id.ToString(),
                AuthorId = even.AuthorId.ToString(),
                Name = even.Name,
                Description = even.Description,
                Status = even.Status,
                StartDate = even.StartDate,
                CreatedAt = even.CreatedAt,
                UpdatedAt = even.UpdatedAt
            };
        }
    }
}
