using BLL.Dto.Reservation;
using BLL.Service.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Realizations
{
    public class BookReservationService : IBookReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookService _bookService;
        private readonly IBookSellingService _bookSellingService;

        public BookReservationService(IUnitOfWork unitOfWork, IBookService bookService, IBookSellingService bookSellingService)
        {
            _unitOfWork = unitOfWork;
            _bookService = bookService;
            _bookSellingService = bookSellingService;
        }

        public async Task<bool> Reserve(ReservationDto reservationDto)
        {
            var reservation = new Reservation
            {
                ExpirationTime = reservationDto.ExpirationTime,
                FullNameOfReservator = reservationDto.FullNameOfReservator,
                UserId = reservationDto.UserId,
                ReservationParts = reservationDto.ReservationParts.Select(rp => new ReservationPart
                {
                    BookId = rp.BookId,
                    Quantity = rp.Quantity,
                }).ToList(),
            };

            foreach (var reservationPart in reservation.ReservationParts)
            {
                if (!await _bookService.DecreaseQuantity(reservationPart.BookId, reservationPart.Quantity))
                {
                    return false;
                }
            }

            await _unitOfWork.Reservation.CreateAsync(reservation);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task Cancel(int id, string userId)
        {
            var reservation = await _unitOfWork.Reservation.GetFirstOrDefaultAsync(r => r.Id == id && r.UserId == userId, rr => rr.Include(r => r.ReservationParts));
            if (reservation == null)
            {
                return;
            }

            foreach (var reservationPart in reservation.ReservationParts)
            {
                await _bookService.IncreaseQuantity(reservationPart.BookId, reservationPart.Quantity);
            }

            _unitOfWork.Reservation.Delete(reservation);
            await _unitOfWork.SaveAsync();
        }

        public async Task Confirm(int id, string userId)
        {
            var reservation = await _unitOfWork.Reservation
                .GetFirstOrDefaultAsync(r => r.Id == id && r.UserId == userId, rr => rr.Include(r => r.ReservationParts));

            await _bookSellingService.CreateOrder(new Dto.Order.OrderDto
            {
                UserId = reservation.UserId,
                OrderParts = reservation.ReservationParts.Select(rp => new Dto.Order.OrderPartDto
                {
                    BookId = rp.BookId,
                    Quantity = rp.Quantity,
                }).ToList(),
            });
            _unitOfWork.Reservation.Delete(reservation);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ReservationBriefInformation>> GetAll(string userId)
        {
            var result = await _unitOfWork.Reservation.GetAllAsync(r => r.UserId == userId);
            return result.Select(r => new ReservationBriefInformation
            {
                Id = r.Id,
                FullNameOfReservator = r.FullNameOfReservator,
            });
        }

        public async Task<ReservationFullInformation?> GetFullInformation(int id, string userId)
        {
            var result = await _unitOfWork.Reservation.GetFirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
            if (result == null)
            {
                return null;
            }
            return new ReservationFullInformation
            {
                ExpirationTime = result.ExpirationTime,
                FullNameOfReservator = result.FullNameOfReservator,
                Id = result.Id,
                ReservationParts = result.ReservationParts.Select(r => new ReservationPartDto
                {
                    BookId = r.BookId,
                    Quantity = r.Quantity,
                }).ToList(),
            };
        }
    }
}
