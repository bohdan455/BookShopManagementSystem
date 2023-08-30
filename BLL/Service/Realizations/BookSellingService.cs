using BLL.Dto.Order;
using BLL.Service.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;

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
                UserId = orderDto.UserId,
                OrderParts = orderDto.OrderParts.Select(op => new OrderPart
                {
                    BookId = op.BookId,
                    Quantity = op.Quantity,

                }).ToList(),
            };

            for (int i = 0; i < order.OrderParts.Count; i++)
            {
                var orderPart = order.OrderParts[i];
                var bookId = orderPart.BookId;
                var book = await _bookService.GetFullInformation(bookId, order.UserId);
                orderPart.TotalPrice = orderPart.Quantity * book.PriceWithDiscount;
            }

            order.TotalPrice = order.OrderParts.Sum(op => op.TotalPrice);
            _unitOfWork.CreateTransaction();
            foreach (var orderPart in order.OrderParts)
            {
                if (!await _bookService.DecreaseQuantity(orderPart.BookId, orderPart.Quantity))
                {
                    _unitOfWork.Rollback();
                    return false;
                }
            }

            if (order.TotalPrice > 0)
            {
                await _unitOfWork.OrderDetails.CreateAsync(order);
                await _unitOfWork.SaveAsync();
                _unitOfWork.Commit();
                return true;
            }

            _unitOfWork.Rollback();
            return false;
        }
    }
}
