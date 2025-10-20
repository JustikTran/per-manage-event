using ManageEventBackend.Applications.DTOs.Event;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Entities;
using ManageEventBackend.Domains.Interfaces;
using ManageEventBackend.Infrastructures.Data;

namespace ManageEventBackend.Infrastructures.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext context;
        public EventRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Response> CreateEvent(CreateEventDto eventDto)
        {
            try
            {
                Event newEvent = new()
                {
                    AuthorId = Guid.Parse(eventDto.AuthorId),
                    Name = eventDto.Name,
                    Status = eventDto.Status,
                    StartDate = eventDto.StartDate
                };

                context.Events.Add(newEvent);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Event created successfully.",
                    Data = newEvent
                };
            }
            catch (Exception err)
            {
                throw new BadHttpRequestException("An occurred error while create new event.", 500, err);
            }
        }

        public async Task<Response> DeleteEvent(Guid eventId)
        {
            try
            {
                var existingEvent = await context.Events.FindAsync(eventId);
                if (existingEvent == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Event not found.",
                        Data = null
                    };
                }
                context.Events.Remove(existingEvent);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Event deleted successfully.",
                    Data = null
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while delete event.", 500, err);
            }
        }

        public IQueryable<EventResponse> GetAllEvent()
        {
            try
            {
                var listEvents = context.Events.ToList();
                return listEvents
                    .AsQueryable().Select(e => EventMapper.Instance.ToResponse(e));
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while get all events.", 500, err);
            }
        }

        public async Task<Response> GetEventById(Guid eventId)
        {
            try
            {
                var existEvent = await context.Events.FindAsync(eventId);
                if (existEvent == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Event not found.",
                        Data = null
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Event retrieved successfully.",
                    Data = EventMapper.Instance.ToResponse(existEvent)
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while get user by id.", 500, err);
            }
        }

        public async Task<Response> UpdateEvent(UpdateEventDto eventDto)
        {
            try
            {
                if (!Guid.TryParse(eventDto.Id, out Guid eventId))
                {
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Invalid event ID format.",
                        Data = null
                    };
                }
                var existing = await context.Events.FindAsync(eventId);
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Event not found.",
                        Data = null
                    };
                }
                existing.Name = eventDto.Name;
                existing.Status = eventDto.Status;
                existing.StartDate = eventDto.StartDate;
                existing.Description = eventDto.Description ?? existing.Description;
                existing.UpdatedAt = DateTime.UtcNow;
                context.Events.Update(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Event updated successfully.",
                    Data = EventMapper.Instance.ToResponse(existing)
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while update event.", 500, err);
            }
        }
    }
}
