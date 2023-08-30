using BLL.Dto.Reservation;

namespace BLL.Service.Interfaces
{
    public interface IBookReservationService
    {
        Task Cancel(int id, string userId);
        Task Confirm(int id, string userId);
        Task<IEnumerable<ReservationBriefInformation>> GetAll(string userId);
        Task<ReservationFullInformation?> GetFullInformation(int id, string userId);
        Task<bool> Reserve(ReservationDto reservationDto);
    }
}