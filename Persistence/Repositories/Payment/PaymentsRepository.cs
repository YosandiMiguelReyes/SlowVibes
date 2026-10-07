

using Domain.Entities.Payment;
using Microsoft.EntityFrameworkCore;
using Persistence.BaseRepository;
using Persistence.Context;
using Application.DTOs.Payment;
using Application.Contracts.Repositories.Payment;
//using Persistence.Mappers.PaymentMappers;

namespace Persistence.Repositories.Payment
{
    public class PaymentsRepository : BaseRepository<Payments, int>, IPaymentsRepository
    {
        public PaymentsRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public Task<IEnumerable<PaymentDTO>> GetPaymentByUserId(int userId)
        {
            throw new NotImplementedException();
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
