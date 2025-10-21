using ManageEventBackend.Applications.DTOs.EventGift;
using ManageEventBackend.Applications.Responses;
using ManageEventBackend.Domains.Entities;
using ManageEventBackend.Domains.Interfaces;
using ManageEventBackend.Infrastructures.Data;

namespace ManageEventBackend.Infrastructures.Repositories
{
    public class EventGiftRepository : IEventGiftRepository
    {
        private readonly AppDbContext context;

        public EventGiftRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Response> CreateGift(CreateEventGiftDto giftDto)
        {
            try
            {
                var newGift = new EventGift
                {
                    EventId = Guid.Parse(giftDto.EventId),
                    Information = giftDto.Information
                };

                context.EventGifts.Add(newGift);
                await context.SaveChangesAsync();

                return new Response
                {
                    StatusCode = 201,
                    Message = "Gift created successfully",
                    Data = EventGiftMapper.Instance.ToResponse(newGift)
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while create gift.", 500, err);
            }
        }

        public async Task<Response> DeleteGift(Guid id)
        {
            try
            {
                var gift = await context.EventGifts.FindAsync(id);
                if (gift == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Gift not found",
                        Data = null
                    };
                }
                context.EventGifts.Remove(gift);
                await context.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 200,
                    Message = "Gift deleted successfully",
                    Data = null
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while delete gift.", 500, err);
            }
        }

        public IQueryable<EventGiftResponse> GetAllGifts()
        {
            try
            {
                var listGifts = context.EventGifts.ToList();
                return listGifts.AsQueryable().Select(gift => EventGiftMapper.Instance.ToResponse(gift));
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while get all gifts.", 500, err);
            }
        }

        public async Task<Response> GetById(Guid id)
        {
            try
            {
                var gift = await context.EventGifts.FindAsync(id);
                if (gift == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Gift not found",
                        Data = null
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Gift retrieved successfully",
                    Data = EventGiftMapper.Instance.ToResponse(gift)
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while get gift by id.", 500, err);
            }
        }

        public async Task<Response> UpdateGift(UpdateEventGiftDto giftDto)
        {
            try
            {
                if (!Guid.TryParse(giftDto.Id, out Guid giftId))
                {
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Invalid gift ID format",
                        Data = null
                    };
                }

                var gift = await context.EventGifts.FindAsync(giftId);
                if (gift == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Gift not found",
                        Data = null
                    };
                }

                gift.Information = giftDto.Information;

                context.EventGifts.Update(gift);
                await context.SaveChangesAsync();

                return new Response
                {
                    StatusCode = 200,
                    Message = "Gift updated successfully",
                    Data = EventGiftMapper.Instance.ToResponse(gift)
                };
            }
            catch (Exception err)
            {

                throw new BadHttpRequestException("An occurred error while update gift.", 500, err);
            }
        }
    }
}
