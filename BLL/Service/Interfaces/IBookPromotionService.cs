using BLL.Dto.Promotion;
using DataAccess.Entities;

namespace BLL.Service.Interfaces
{
    public interface IBookPromotionService
    {
        Task<decimal> CalculateDiscountOfBook(Book book);
        Task Create(PromotionDto promotionDto);
        Task Delete(int id, string userId);
        Task<IEnumerable<PromotionBriefInformation>> GetAll(string userId);
    }
}