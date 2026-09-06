

using Domain.Entities.Order;
using Domain.Entities.User;
using Domain.Entities.Order.Enums;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.Order;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Order
{
    internal class OrderRepository : BaseRepository<Orders, int>, IOrderRepository
    {
        public OrderRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Orders>> GetOrderByDateRange(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetOrdersByCustomerPhoneAsync(string customerPhone)
        {
            return await(
                from order in _dbSet.AsNoTracking()
                join user in _context.Set<Domain.Entities.User.Users>().AsNoTracking()
                on order.UserId equals user.Id
                where user.Phone == customerPhone
                select order).ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetOrdersByDeliveryTypeAsync(DeliveryTypes deliveryType)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(o => o.DeliveryType == deliveryType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetOrdersBySourceAsync(OrderSources source)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(o => o.OrderSource == source)
                .ToListAsync();
        }
        

        public async Task<IEnumerable<Orders>> GetOrdersByStatusAsync(OrderStatuses status)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(o => o.OrderStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetOrdersByTotalAmountRangeAsync(decimal minAmount, decimal maxAmount)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(o => o.TotalAmount >= minAmount && o.TotalAmount <= maxAmount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetOrdersByTotalProfitRangeAsync(decimal minAmount, decimal maxAmount)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(o => o.TotalProfit >= minAmount && o.TotalProfit <= maxAmount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetOrdersByUserNameAsync(string userName)
        {
            return await(
                from order in _dbSet.AsNoTracking()
                join user in _context.Set<Domain.Entities.User.Users>().AsNoTracking()
                on order.UserId equals user.Id
                where user.UserName == userName
                select order).ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetOrdersOrderByDateAsync(bool ascending)
        {
            var query = _dbSet.AsNoTracking();

            query = ascending ? query.OrderBy(o => o.OrderDate) : query.OrderByDescending(o => o.OrderDate);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetOrdersOrderByTotalAmountAsync(bool ascending)
        {
            var query = _dbSet.AsNoTracking();

            query = ascending ? query.OrderBy(o => o.TotalAmount) : query.OrderByDescending(o => o.TotalAmount);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Orders>> GetOrdersOrderByTotalProfitAsync(bool ascending)
        {
            var query = _dbSet.AsNoTracking();

            query = ascending ? query.OrderBy(o => o.TotalProfit) : query.OrderByDescending(o => o.TotalProfit);
            return await query.ToListAsync();
        }
    }
}
