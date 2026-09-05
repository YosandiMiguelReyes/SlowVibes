

using Domain.Entities.Order;
using Domain.Entities.User;
using Domain.Entities.Order.Enums;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.Order;

namespace Persistence.Repositories.Order
{
    internal class OrderRepository : BaseRepository<Orders, int>, IOrderRepository
    {
        public OrderRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public Task<IEnumerable<Orders>> GetOrderByDateRange(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersByCustomerPhoneAsync(string customerPhone)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersByDeliveryTypeAsync(DeliveryTypes deliveryType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersBySourceAsync(OrderSources source)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersByStatusAsync(OrderStatuses status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersByTotalAmountRangeAsync(decimal minAmount, decimal maxAmount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersByTotalProfitRangeAsync(decimal minAmount, decimal maxAmount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersByUserNameAsync(string userName)
        {
            var query = 
                from order in _dbSet
                join user in _context.Set<Users>()
                on order.UserId equals user.Id
                where(userName == user.userName)
                select order;
        }

        public Task<IEnumerable<Orders>> GetOrdersOrderByDateAsync(bool ascending)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersOrderByTotalAmountAsync(bool ascending)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersOrderByTotalProfitAsync(bool ascending)
        {
            throw new NotImplementedException();
        }
    }
}
