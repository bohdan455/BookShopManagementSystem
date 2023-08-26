using BLL.Dto.Order;
using BLL.Service.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Realizations
{
    public class BookSellingService : IBookSellingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookService _bookService;

        public BookSellingService(IUnitOfWork unitOfWork, IBookService bookService)
        {
            _unitOfWork = unitOfWork;
            _bookService = bookService;
        }
        public async Task<bool> CreateOrder(OrderDto orderDto)
        {
            var order = new OrderDetails
            {
                DateOfPurchase = DateTime.Now,
                TotalPrice = orderDto.OrderParts.Sum(op => op.Quantity * op.PriceForItem),
                UserId = orderDto.UserId,
                OrderParts = orderDto.OrderParts.Select(op => new OrderPart
                {
                    BookId = op.BookId,
                    Quantity = op.Quantity,
                    TotalPrice = op.PriceForItem * op.Quantity,

                }).ToList(),
            };

            foreach (var orderPart in order.OrderParts)
            {
                if (!await _bookService.DecreaseQuantity(orderPart.BookId, orderPart.Quantity))
                {
                    return false;
                }
            }
            await _unitOfWork.OrderDetails.CreateAsync(order);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}
