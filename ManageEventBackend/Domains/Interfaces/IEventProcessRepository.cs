using ManageEventBackend.Applications.DTOs.EventProcess;
using ManageEventBackend.Applications.Responses;

namespace ManageEventBackend.Domains.Interfaces
{
    public interface IEventProcessRepository
    {
        IQueryable<EventProcessResponse> GetAllProcesses();
        Task<Response> GetProcessById(Guid processId);
        Task<Response> CreateProcess(CreateEventProcessDto processDto);
        Task<Response> UpdateProcess(UpdateEventProcessDto processDto);
        Task<Response> DeleteProcess(Guid processId);
    }
}
