

using Domain.Entities.Payment;
using Persistence.BaseRepository;
using Persistence.Interfaces.Payment;

namespace Persistence.Repositories.Payment
{
    public class PaymentsRepository : BaseRepository<Payments, int>, IPaymentsRepository
    {
        public Task<IEnumerable<Payments>> GetPaymentsByAmountRangeAsync(decimal minAmount, decimal maxAmount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payments>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payments>> GetPaymentsByMethodAsync(string method)
        {
            throw new NotImplementedException();
        }

        public Task<Payments?> GetPaymentsByOrderIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Payments?> GetPaymentsByReferenceNumberAsync(string referenceNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payments>> GetPaymentsByStatusAsync(string status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payments>> GetPaymentsByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payments>> GetPaymentsOrderedByAmountAsync(bool ascending)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Payments>> GetPaymentsOrderedByDateAsync(bool ascending)
        {
            throw new NotImplementedException();
        }
    }
}
