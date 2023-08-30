using BLL.Dto.Book;
using BLL.Service.Interfaces;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Realizations
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookPromotionService _bookPromotionService;

        public BookService(IUnitOfWork unitOfWork, IBookPromotionService bookPromotionService)
        {
            _unitOfWork = unitOfWork;
            _bookPromotionService = bookPromotionService;
        }

        public async Task Add(BookDto bookDto)
        {
            var book = new Book
            {
                DateOfAdding = DateTime.UtcNow,
                NumberOfPages = bookDto.NumberOfPages,
                PreviousBookId = bookDto.PreviousBookId,
                Year = bookDto.Year,
                UserId = bookDto.UserId,
                Quantity = bookDto.Quantity,
                SellingPrice = bookDto.SellingPrice,
                ProductionPrice = bookDto.ProductionPrice,
                Title = bookDto.Title,
                GenreId = await GetOrCreateGenre(bookDto.Genre,bookDto.UserId),
                PublisherId = await GetOrCreatePublisher(bookDto.Publisher, bookDto.UserId),
                AuthorId = await GetOrCreateAuthor(bookDto.Author, bookDto.UserId),
            };

            await _unitOfWork.Book.CreateAsync(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(BookDto bookDto)
        {
            var book = new Book
            {
                Id = bookDto.Id,
                NumberOfPages = bookDto.NumberOfPages,
                PreviousBookId = bookDto.PreviousBookId,
                Year = bookDto.Year,
                UserId = bookDto.UserId,
                Quantity = bookDto.Quantity,
                SellingPrice = bookDto.SellingPrice,
                ProductionPrice = bookDto.ProductionPrice,
                Title = bookDto.Title,
                GenreId = await GetOrCreateGenre(bookDto.Genre, bookDto.UserId),
                PublisherId = await GetOrCreatePublisher(bookDto.Publisher, bookDto.UserId),
                AuthorId = await GetOrCreateAuthor(bookDto.Author, bookDto.UserId),
            };

            _unitOfWork.Book.Update(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id, string userId)
        {
            var book = await _unitOfWork.Book.GetFirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (book == null)
            {
                return;
            }

            _unitOfWork.Book.Delete(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BookBriefInformation>> Search(string userId, string searchRequest = "")
        {
            var result = await _unitOfWork.Book.GetAllAsync(
                b => (b.Title.Contains(searchRequest) || b.Author.Name.Contains(searchRequest) || b.Genre.Name.Contains(searchRequest)) && b.UserId == userId,
                br => br.Include(b => b.Author).Include(b => b.Genre));

            return result.Select(b => new BookBriefInformation
            {
                Author = b.Author.Name,
                Genre = b.Genre.Name,
                Title = b.Title,
                Id = b.Id,
                SellingPrice = b.SellingPrice,
                Year = b.Year,
                Quantity = b.Quantity,
            });
        }

        public async Task<BookFullInformation?> GetFullInformation(int id, string userId)
        {
            var bookFromDb = await _unitOfWork.Book.GetFirstOrDefaultAsync(predicate: b => b.Id == id && b.UserId == userId,
                br => br.Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.PreviousBook!)
                .Include(b => b.Discount!));

            if (bookFromDb == null)
            {
                return null;
            }

            var resultBook = new BookFullInformation
            {
                Id = bookFromDb.Id,
                NumberOfPages = bookFromDb.NumberOfPages,
                Author = bookFromDb.Author.Name,
                Genre = bookFromDb.Genre.Name,
                Publisher = bookFromDb.Publisher.Name,
                Quantity = bookFromDb.Quantity,
                ProductionPrice = bookFromDb.ProductionPrice,
                SellingPrice = bookFromDb.SellingPrice,
                PriceWithDiscount = await _bookPromotionService.CalculateDiscountOfBook(bookFromDb),
                Title = bookFromDb.Title,
                Year = bookFromDb.Year,
            };

            if (bookFromDb.PreviousBook != null)
            {
                bookFromDb.PreviousBook = await _unitOfWork.Book.GetFirstAsync(b => b.Id == bookFromDb.PreviousBook.Id,
                    br => br.Include(b => b.Author).Include(b => b.Genre));

                resultBook.PreviousBook = new BookBriefInformation
                {
                    Title = bookFromDb.PreviousBook!.Title,
                    Author = bookFromDb.PreviousBook.Author.Name,
                    Genre = bookFromDb.PreviousBook.Genre.Name,
                    SellingPrice = bookFromDb.PreviousBook.SellingPrice,
                    Year = bookFromDb.PreviousBook.Year,
                    Id = bookFromDb.PreviousBook.Id,
                    Quantity = bookFromDb.PreviousBook.Quantity,
                };
            }

            return resultBook;
        }

        public async Task<bool> DecreaseQuantity(int bookId, int quantity)
        {
            var book = await _unitOfWork.Book.GetFirstAsync(b => b.Id == bookId);
            if (book.Quantity < quantity)
            {
                return false;
            }
            else
            {
                book.Quantity -= quantity;
                _unitOfWork.Book.Update(book);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }

        public async Task IncreaseQuantity(int bookId, int quantity)
        {
            await _unitOfWork.Book.FindByCondition(b => b.Id == bookId).ExecuteUpdateAsync(u => u.SetProperty(b => b.Quantity, b => b.Quantity + quantity));
        }

        private async Task<int> GetOrCreateAuthor(string authorName,string userId)
        {
            var author = await _unitOfWork.Author.GetFirstOrDefaultAsync(x => x.Name == authorName);
            if (author == null)
            {
                author = new Author
                {
                    Name = authorName,
                    UserId = userId,
                };

                await _unitOfWork.Author.CreateAsync(author);
                await _unitOfWork.SaveAsync();
            }

            return author.Id;
        }

        private async Task<int> GetOrCreateGenre(string genreName, string userId)
        {
            var genre = await _unitOfWork.Genre.GetFirstOrDefaultAsync(x => x.Name == genreName);
            if (genre == null)
            {
                genre = new Genre
                {
                    Name = genreName,
                    UserId = userId,
                };

                await _unitOfWork.Genre.CreateAsync(genre);
                await _unitOfWork.SaveAsync();
            }

            return genre.Id;
        }

        private async Task<int> GetOrCreatePublisher(string publisherName, string userId)
        {
            var publisher = await _unitOfWork.Publisher.GetFirstOrDefaultAsync(x => x.Name == publisherName);
            if (publisher == null)
            {
                publisher = new Publisher
                {
                    Name = publisherName,
                    UserId = userId,
                };

                await _unitOfWork.Publisher.CreateAsync(publisher);
                await _unitOfWork.SaveAsync();
            }

            return publisher.Id;
        }
    }
}
