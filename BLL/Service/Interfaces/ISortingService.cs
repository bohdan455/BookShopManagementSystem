using BLL.Dto.Book;
using DataAccess.Entities;

namespace BLL.Service.Interfaces
{
    public interface ISortingService
    {
        Task<IEnumerable<Author?>> SortAuthorsByPopularity(string userId);
        Task<IEnumerable<BookBriefInformation?>> SortBooksByPopularity(string userId);
        Task<IEnumerable<Genre?>> SortGenresByPopularity(string userId);
    }
}