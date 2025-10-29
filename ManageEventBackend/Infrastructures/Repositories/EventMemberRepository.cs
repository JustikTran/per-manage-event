using ManageEventBackend.Applications.DTOs.EventMember;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Entities;
using ManageEventBackend.Domains.Interfaces;
using ManageEventBackend.Infrastructures.Data;

namespace ManageEventBackend.Infrastructures.Repositories
{
    public class EventMemberRepository : IEventMemberRepository
    {
        private readonly AppDbContext context;
        public EventMemberRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Response> AddParticipant(CreateEventMemberDto memberDto)
        {
            try
            {
                var member = new EventMember
                {
                    EventId = Guid.Parse(memberDto.EventId),
                    Name = memberDto.Name,
                    Nickname = memberDto.Nickname!,
                    Email = memberDto.Email!,
                    Phone = memberDto.Phone!
                };
                context.EventMembers.Add(member);
                await context.SaveChangesAsync();

                return new Response
                {
                    StatusCode = 201,
                    Message = "New member added successfully."
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while add new member.", 500, err);
            }
        }

        public async Task<Response> AddParticipants(List<CreateEventMemberDto> memberDtos)
        {
            try
            {
                List<EventMember> eventMembers = new();
                foreach (var item in eventMembers)
                {
                    var temp = new EventMember
                    {
                        EventId = item.EventId,
                        Name = item.Name,
                        Nickname = item.Nickname!,
                        Email = item.Email!,
                        Phone = item.Phone!
                    };
                    eventMembers.Add(temp);
                }

                context.EventMembers.AddRange(eventMembers);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 201,
                    Message = "List members added successfully."
                };
            }
            catch (Exception err)
            {
                throw new BadHttpRequestException("An occurred error while add list member of event.", 500, err);
            }
        }

        public async Task<Response> DeleteParticipant(Guid id)
        {
            try
            {
                var existing = await context.EventMembers.FindAsync(id);
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Member not found."
                    };
                }
                context.EventMembers.Remove(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Member deleted successfully."
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while delete member.", 500, err);
            }
        }

        public IQueryable<EventMemberResponse> GetAllParticipants()
        {
            try
            {
                var listMembers = context.EventMembers.ToList();
                return listMembers.AsQueryable()
                    .Select(em => EventMemberMapper.Instance.ToResponse(em));
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while get all members.", 500, err);
            }
        }

        public async Task<Response> GetParticipantById(Guid id)
        {
            try
            {
                var existing = await context.EventMembers.FindAsync(id);
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Member not found."
                    };
                }

                return new Response
                {
                    StatusCode = 200,
                    Message = "Member retrieved successfully.",
                    Data = EventMemberMapper.Instance.ToResponse(existing)
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while get by id.", 500, err);
            }
        }

        public async Task<Response> UpdateParticipant(UpdateEventMemberDto memberDto)
        {
            try
            {
                if (!Guid.TryParse(memberDto.Id, out Guid id))
                {
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Invalid member ID format."
                    };
                }
                var existing = await context.EventMembers.FindAsync(id);
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Member not found."
                    };
                }
                existing.Name = memberDto.Name;
                existing.Nickname = memberDto.Nickname!;
                existing.Email = memberDto.Email!;
                existing.Phone = memberDto.Phone!;
                context.EventMembers.Update(existing);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Member updated successfully."
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while update member.", 500, err);
            }
        }
    }
}
