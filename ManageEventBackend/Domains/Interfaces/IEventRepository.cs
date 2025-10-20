using ManageEventBackend.Applications.DTOs.Event;
using ManageEventBackend.Applications.Responses;

namespace ManageEventBackend.Domains.Interfaces
{
    public interface IEventRepository
    {
        IQueryable<EventResponse> GetAllEvent();
        Task<Response> GetEventById(Guid eventId);
        Task<Response> CreateEvent(CreateEventDto eventDto);
        Task<Response> UpdateEvent(UpdateEventDto eventDto);
        Task<Response> DeleteEvent(Guid eventId);
    }
}
