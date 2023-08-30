using BLL.Dto.Book;
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
    // TODO change this 
    public class SortingService : ISortingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SortingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Author?>> SortAuthorsByPopularity(string userId)
        {
            //TODO Check if this causes null error
            var result = await _unitOfWork.Author.GetAllAsync(a => a.UserId == userId, ar => ar.Include(a => a.Books).ThenInclude(b => b.OrderParts));
            return result.OrderByDescending(a => a?.Books.Sum(b => b.OrderParts.Sum(op => op.Quantity)));
        }

        public async Task<IEnumerable<Genre?>> SortGenresByPopularity(string userId)
        {
            //TODO Check if this causes null error
            var result = await _unitOfWork.Genre.GetAllAsync(g => g.UserId == userId, gr => gr.Include(g => g.Books).ThenInclude(g => g.OrderParts));
            return result.OrderByDescending(g => g?.Books.Sum(b => b.OrderParts.Sum(op => op.Quantity)));
        }

        public async Task<IEnumerable<BookBriefInformation?>> SortBooksByPopularity(string userId)
        {
            var result = await _unitOfWork.Book.GetAllAsync(b => b.UserId == userId,
                br => br.Include(b => b.OrderParts).Include(b => b.Author).Include(b => b.Genre));

            return result.OrderBy(b => b.OrderParts.Sum(od => od.Quantity)).Select(b => new BookBriefInformation
            {
                Author = b.Author.Name,
                Genre = b.Genre.Name,
                Id = b.Id,
                SellingPrice = b.SellingPrice,
                Title = b.Title,
                Year = b.Year,
            });
        }
    }
}
