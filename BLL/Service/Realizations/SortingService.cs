using BLL.Dto;
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
    public class SortingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SortingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Author>> SortAuthorsByPopularity(string userId)
        {

            var result = await _unitOfWork.Author.GetAllAsync(a => a.UserId == userId,ar => ar.Include(a => a.Books).ThenInclude(b => b.OrderParts));
                
        }

        public Task<IEnumerable<Genre>> SortGenresByPopularity(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookBriefInformation>> SortBooksByPopularity(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
