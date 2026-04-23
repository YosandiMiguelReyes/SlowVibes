

using Domain.Entities.Payment;
using Domain.Repositories;

namespace Persistence.Interfaces.Payment
{
    public interface IPaymentsRepository : IBaseRepository<Payments, int>
    {
        // al queries responses must have order and user
        Task<IEnumerable<Payments>> GetPaymentsByUserNameAsync(string userName);
        Task<Payments?> GetPaymentsByOrderIdAsync(int orderId);
        Task<IEnumerable<Payments>> GetPaymentsByMethodAsync(string method);
        Task<IEnumerable<Payments>> GetPaymentsByAmountRangeAsync(decimal minAmount, decimal maxAmount);
        Task<IEnumerable<Payments>> GetPaymentsByStatusAsync(string status);
        Task<Payments?> GetPaymentsByReferenceNumberAsync(string referenceNumber);
        Task<IEnumerable<Payments>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Payments>> GetPaymentsOrderedByAmountAsync(bool ascending);
        Task<IEnumerable<Payments>> GetPaymentsOrderedByDateAsync(bool ascending);

    }
}
