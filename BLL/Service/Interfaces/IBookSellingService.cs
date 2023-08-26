using BLL.Dto.Order;

namespace BLL.Service.Interfaces
{
    public interface IBookSellingService
    {
        Task<bool> CreateOrder(OrderDto orderDto);
    }
}