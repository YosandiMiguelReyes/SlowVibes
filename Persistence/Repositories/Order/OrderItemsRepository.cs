

using Domain.Entities.Order;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.Order;

namespace Persistence.Repositories.Order
{
    public class OrderItemsRepository : BaseRepository<OrderItems, int>, IOrderItemsRepository
    {
        public OrderItemsRepository(SlowVibesDbContext context) : base(context)
        {
            
        }
        public Task<IEnumerable<OrderItems>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
