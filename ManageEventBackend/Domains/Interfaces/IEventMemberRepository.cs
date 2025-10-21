using ManageEventBackend.Applications.DTOs.EventMember;
using ManageEventBackend.Applications.Responses;

namespace ManageEventBackend.Domains.Interfaces
{
    public interface IEventMemberRepository
    {
        IQueryable<EventMemberResponse> GetAllParticipants();
        Task<Response> GetParticipantById(Guid id);
        Task<Response> AddParticipant(CreateEventMemberDto memberDto);
        Task<Response> UpdateParticipant(UpdateEventMemberDto memberDto);
        Task<Response> DeleteParticipant(Guid id);
    }
}
