

using Domain.Entities.Payment;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.DTO.Product;
using Persistence.Interfaces.Payment;
using Persistence.Mappers.PaymentMappers;

namespace Persistence.Repositories.Payment
{
    public class PaymentsRepository : BaseRepository<Payments, int>, IPaymentsRepository
    {
        public PaymentsRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentByUserId(int userId)
        {
            return await _dbSet
            .Where(p => p.order.User.Id == userId)
            .Select(PaymentsMapper.AsPaymentDTO)
            .ToListAsync();
        }

        public Task<IEnumerable<PaymentDTO>> GetPaymentsByAmountRangeAsync(decimal minAmount, decimal maxAmount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentDTO>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentDTO>> GetPaymentsByMethodAsync(string method)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentDTO?> GetPaymentsByOrderIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentDTO?> GetPaymentsByReferenceNumberAsync(string referenceNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentDTO>> GetPaymentsByStatusAsync(string status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentDTO>> GetPaymentsByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentDTO>> GetPaymentsOrderedByAmountAsync(bool ascending)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentDTO>> GetPaymentsOrderedByDateAsync(bool ascending)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetTotalPaidByOrderIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
