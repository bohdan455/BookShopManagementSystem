using BLL.Dto.Reservation;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Realizations
{
    public class BookReservationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Reserve(ReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }

        public Task CancelReservation(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task ConfirmReservation(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task GetAllReservations(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
