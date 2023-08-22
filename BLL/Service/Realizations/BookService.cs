using BLL.Dto;
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
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(BookDto bookDto)
        {
            var book = new Book
            {
                DateOfAdding = DateTime.Now,
                NumberOfPages = bookDto.NumberOfPages,
                PreviousBookId = bookDto.PreviousBookId,
                Year = bookDto.Year,
                UserId = bookDto.UserId,
                Quantity = bookDto.Quantity,
                SellingPrice = bookDto.SellingPrice,
                ProductionPrice = bookDto.ProductionPrice,
                Title = bookDto.Title,
                GenreId = await GetOrCreateGenre(bookDto.Genre),
                PublisherId = await GetOrCreatePublisher(bookDto.Publisher),
                AuthorId = await GetOrCreateAuthor(bookDto.Author),
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
                GenreId = await GetOrCreateGenre(bookDto.Genre),
                PublisherId = await GetOrCreatePublisher(bookDto.Publisher),
                AuthorId = await GetOrCreateAuthor(bookDto.Author),
            };

            _unitOfWork.Book.Update(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id,string userId)
        {
            var book = await _unitOfWork.Book.GetFirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (book == null)
            {
                return;
            }

            _unitOfWork.Book.Delete(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<BookBriefInformation>> Search(string searchRequest, string userId)
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
                Year = b.Year
            });
        }

        public async Task<BookFullInformation?> GetFullInformation(int id,string userId)
        {
            var bookFromDb = await _unitOfWork.Book.GetFirstOrDefaultAsync(predicate: b => b.Id == id && b.UserId == userId, 
                br => br.Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.PreviousBook));

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
                };
            }

            return resultBook;
        }

        private async Task<int> GetOrCreateAuthor(string authorName)
        {
            var author = await _unitOfWork.Author.GetFirstOrDefaultAsync(x => x.Name == authorName);
            if (author == null)
            {
                author = new Author
                {
                    Name = authorName,
                };

                await _unitOfWork.Author.CreateAsync(author);
                await _unitOfWork.SaveAsync();
            }

            return author.Id;
        }

        private async Task<int> GetOrCreateGenre(string genreName)
        {
            var genre = await _unitOfWork.Genre.GetFirstOrDefaultAsync(x => x.Name == genreName);
            if (genre == null)
            {
                genre = new Genre
                {
                    Name = genreName,
                };

                await _unitOfWork.Genre.CreateAsync(genre);
                await _unitOfWork.SaveAsync();
            }

            return genre.Id;
        }

        private async Task<int> GetOrCreatePublisher(string publisherName)
        {
            var publisher = await _unitOfWork.Publisher.GetFirstOrDefaultAsync(x => x.Name == publisherName);
            if (publisher == null)
            {
                publisher = new Publisher
                {
                    Name = publisherName,
                };

                await _unitOfWork.Publisher.CreateAsync(publisher);
                await _unitOfWork.SaveAsync();
            }

            return publisher.Id;
        }
    }
}
