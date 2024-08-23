using Core.Dtos;

namespace Core.Interfaces
{
    public interface IOrdersService
    {
        List<OrderDto> GetOrders(string userId);
        Task Create(string userId, string userEmail);
    }
}
