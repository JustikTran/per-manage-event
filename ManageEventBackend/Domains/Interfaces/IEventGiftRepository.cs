using ManageEventBackend.Applications.DTOs.EventGift;
using ManageEventBackend.Applications.Responses;

namespace ManageEventBackend.Domains.Interfaces
{
    public interface IEventGiftRepository
    {
        IQueryable<EventGiftResponse> GetAllGifts();
        Task<Response> GetById(Guid id);
        Task<Response> CreateGift(CreateEventGiftDto giftDto);
        Task<Response> UpdateGift(UpdateEventGiftDto giftDto);
        Task<Response> DeleteGift(Guid id);
    }
}
