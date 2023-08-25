using BLL.Dto.Book;

namespace BLL.Service.Interfaces
{
    public interface IBookService
    {
        Task Add(BookDto bookDto);
        Task<bool> DecreaseQuantity(int bookId, int quantity);
        Task Delete(int id, string userId);
        Task<BookFullInformation?> GetFullInformation(int id, string userId);
        Task IncreaseQuantity(int bookId, int quantity);
        Task<IEnumerable<BookBriefInformation>> Search(string searchRequest, string userId);
        Task Update(BookDto bookDto);
    }
}