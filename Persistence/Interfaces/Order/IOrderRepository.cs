
using Domain.Entities.Order;
using Domain.Repositories;

namespace Persistence.Interfaces.Order
{
    public interface IOrderRepository : IBaseRepository<Orders, int>
    {
        //just shows a list of orders with the user name, no need to add the items of the order, if user wants to see S/he can click on the order and see the details of the order.

        Task<IEnumerable<Orders>> GetOrdersByUserNameAsync(string userName);
        Task<IEnumerable<Orders>> GetOrdersByStatusAsync(string status);
        Task<IEnumerable<Orders>> GetOrdersBySourceAsync(string source);
        Task<IEnumerable<Orders>> GetOrdersByDeliveryTypeAsync(string deliveryType);
        Task<IEnumerable<Orders>> GetOrdersByCustomerPhoneAsync(string customerPhone);


        Task<IEnumerable<Orders>> GetOrderByDateRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Orders>> GetOrdersByTotalAmountRangeAsync(decimal minAmount, decimal maxAmount);
        Task<IEnumerable<Orders>> GetOrdersByTotalProfitRangeAsync(decimal minAmount, decimal maxAmount);

        Task<IEnumerable<Orders>> GetOrdersOrderByTotalProfitAsync(bool ascending);
        Task<IEnumerable<Orders>> GetOrdersOrderByTotalAmountAsync(bool ascending);
        Task<IEnumerable<Orders>> GetOrdersOrderByDateAsync(bool ascending);
    }
}
