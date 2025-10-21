using ManageEventBackend.Applications.DTOs.EventProcess;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Entities;
using ManageEventBackend.Domains.Interfaces;
using ManageEventBackend.Infrastructures.Data;

namespace ManageEventBackend.Infrastructures.Repositories
{
    public class EventProcessRepository : IEventProcessRepository
    {
        private readonly AppDbContext context;
        public EventProcessRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Response> CreateProcess(CreateEventProcessDto processDto)
        {
            try
            {
                var newProcess = new EventProcess
                {
                    EventId = Guid.Parse(processDto.EventId),
                    Content = processDto.Content,
                    StartTime = processDto.StartTime,
                    Status = processDto.Status,
                    ExtendedTime = processDto.ExtendedTime
                };
                context.EventProcesses.Add(newProcess);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Process created successfully.",
                    Data = EventProcessMapper.Instance.ToResponse(newProcess)
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while create process.", 500, err);
            }
        }

        public async Task<Response> DeleteProcess(Guid processId)
        {
            try
            {
                var existingProcess = await context.EventProcesses.FindAsync(processId);
                if (existingProcess == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Process not found.",
                        Data = null
                    };
                }
                context.EventProcesses.Remove(existingProcess);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Process deleted successfully.",
                    Data = null
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while delete process.", 500, err);
            }
        }

        public IQueryable<EventProcessResponse> GetAllProcesses()
        {
            try
            {
                var listProcesses = context.EventProcesses.ToList();
                return listProcesses.AsQueryable().Select(process => EventProcessMapper.Instance.ToResponse(process));
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while get all processes.", 500, err);
            }
        }

        public async Task<Response> GetProcessById(Guid processId)
        {
            try
            {
                var process = await context.EventProcesses.FindAsync(processId);
                if (process == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Process not found.",
                        Data = null
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Process retrieved successfully.",
                    Data = EventProcessMapper.Instance.ToResponse(process)
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while get process.", 500, err);
            }
        }

        public async Task<Response> UpdateProcess(UpdateEventProcessDto processDto)
        {
            try
            {
                if (!Guid.TryParse(processDto.Id, out Guid processId))
                {
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Invalid GUID format.",
                        Data = null
                    };
                }
                var existingProcess = await context.EventProcesses.FindAsync(processId);
                if (existingProcess == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Process not found.",
                        Data = null
                    };
                }
                existingProcess.Content = processDto.Content;
                existingProcess.StartTime = processDto.StartTime;
                existingProcess.Status = processDto.Status;
                existingProcess.ExtendedTime = processDto.ExtendedTime;

                context.EventProcesses.Update(existingProcess);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Process updated successfully.",
                    Data = EventProcessMapper.Instance.ToResponse(existingProcess)
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while update process.", 500, err);
            }
        }
    }
}
