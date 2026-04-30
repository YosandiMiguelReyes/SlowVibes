using Domain.Entities.Order;
using Domain.Repositories;

namespace Persistence.Interfaces.Order
{
    public interface IOrderItemsRepository : IBaseRepository<OrderItems, int>
    {
        Task<IEnumerable<OrderItems>> GetOrderItemsByOrderIdAsync(int orderId);
    }
}
