

using Domain.Entities.Order;
using Persistence.BaseRepository;
using Persistence.Interfaces.Order;

namespace Persistence.Repositories.Order
{
    internal class OrderReository : BaseRepository<Orders, int>, IOrderRepository
    {
        public Task<IEnumerable<Orders>> GetOrderByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersByCustomerPhoneAsync(string customerPhone)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersByDeliveryTypeAsync(string deliveryType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersBySourceAsync(string source)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetOrdersByStatusAsync(string status)
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
            throw new NotImplementedException();
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
