using BLL.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models.Book;
using System.Security.Claims;

namespace Mvc.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;
        private readonly ISortingService _sortingService;

        public BookController(ILogger<BookController> logger,
            IBookService bookService,
            ISortingService sortingService)
        {
            _logger = logger;
            _bookService = bookService;
            _sortingService = sortingService;
        }

        public async Task<IActionResult> ListOfBooks(string searchValue)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var books = await _bookService.Search(userId,searchRequest: searchValue ?? "");
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> NewBook()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var books = await _bookService.Search(userId);
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> NewBook(NewBookModel newBookModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var books = await _bookService.Search(userId);

            if (!ModelState.IsValid)
            {
                ViewData["Added Info"] = "";
                return View(books);
            }

            await _bookService.Add(new BLL.Dto.Book.BookDto
            {
                Author = newBookModel.Author,
                Title = newBookModel.Title,
                Genre = newBookModel.Genre,
                NumberOfPages = newBookModel.NumberOfPages,
                PreviousBookId = newBookModel.PreviousBookId,
                ProductionPrice = newBookModel.ProductionPrice,
                Publisher = newBookModel.Publisher,
                Quantity = newBookModel.Quantity,
                SellingPrice = newBookModel.SellingPrice,
                UserId = userId,
                Year = newBookModel.Year,
            });

            ViewData["Added Info"] =  $"{newBookModel.Title} was added";
            return View(books);
        }

        public async Task<IActionResult> BookFullInformation(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var book = await _bookService.GetFullInformation(id,userId);
            return View(book);
        }

        public async Task<IActionResult> BestBooks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var bestBooks = await _sortingService.SortBooksByPopularity(userId);

            return View(bestBooks);
        }
    }
}
