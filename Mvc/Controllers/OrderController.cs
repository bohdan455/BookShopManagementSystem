using BLL.Dto.Order;
using BLL.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models.Order;
using System.Security.Claims;

namespace Mvc.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBookSellingService _bookSellingService;

        public OrderController(IBookService bookService,IBookSellingService bookSellingService)
        {
            _bookService = bookService;
            _bookSellingService = bookSellingService;
        }

        public async Task<IActionResult> ListOfOrders()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NewOrder()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var books = await _bookService.Search(userId);
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder([FromBody]List<NewOrderPartModel> orderParts)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await _bookSellingService.CreateOrder(new OrderDto
            {
                UserId = userId,
                OrderParts = orderParts.Select(op => new OrderPartDto
                {
                    BookId = op.BookId,
                    Quantity = op.Quantity,
                }).ToList()
            });

            var books = await _bookService.Search(userId);
            return View(books);
        }
    }
}
