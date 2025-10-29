using ManageEventBackend.Domains.Entities;
using System.Text.Json.Serialization;

namespace ManageEventBackend.Applications.Responses
{
    public class EventResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("authorId")]
        public string? AuthorId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")]
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
                Location = even.Location,
                CreatedAt = even.CreatedAt,
                UpdatedAt = even.UpdatedAt
            };
        }
    }
}
