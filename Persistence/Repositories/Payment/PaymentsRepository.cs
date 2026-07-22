

using Domain.Entities.Payment;
using Microsoft.EntityFrameworkCore;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.DTO.Payment;
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
            .Where(p => p.order.UserId == userId)
            .Select(PaymentsMapper.AsPaymentDTO)
            .ToListAsync();
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsByAmountRangeAsync(decimal minAmount, decimal maxAmount)
        {
            return await _dbSet
            .Where(p => p.Amount >= minAmount && p.Amount <= maxAmount)
            .Select(PaymentsMapper.AsPaymentDTO)
            .ToListAsync();
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
            .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
            .Select(PaymentsMapper.AsPaymentDTO)
            .ToListAsync();
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsByMethodAsync(string method)
        {
            return await _dbSet
            .Where(p => p.PaymentMethod.Name == method)
            .Select(PaymentsMapper.AsPaymentDTO)
            .ToListAsync();
        }

        public async Task<PaymentDTO?> GetPaymentsByOrderIdAsync(int orderId)
        {
            return await _dbSet
            .Where(p => p.OrderId == orderId)
            .Select(PaymentsMapper.AsPaymentDTO)
            .FirstOrDefaultAsync();
        }

        public async Task<PaymentDTO?> GetPaymentsByReferenceNumberAsync(string referenceNumber)
        {
            return await _dbSet
            .Where(p => p.ReferenceNumber == referenceNumber)
            .Select(PaymentsMapper.AsPaymentDTO)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsByStatusAsync(string status)
        {
            return await _dbSet
            .Where(p => p.Status == status)
            .Select(PaymentsMapper.AsPaymentDTO)
            .ToListAsync();
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsByUserNameAsync(string userName)
        {
            return await _dbSet
            .Where(p => p.order.User.UserName == userName)
            .Select(PaymentsMapper.AsPaymentDTO)
            .ToListAsync();
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsOrderedByAmountAsync(bool ascending)
        {
            if (ascending)
            {
                return await _dbSet
                .OrderBy(p => p.Amount)
                .Select(PaymentsMapper.AsPaymentDTO)
                .ToListAsync();
            }
            else
            {
                return await _dbSet
                .OrderByDescending(p => p.Amount)
                .Select(PaymentsMapper.AsPaymentDTO)
                .ToListAsync();
            }
            
            
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsOrderedByDateAsync(bool ascending)
        {
            if (ascending)
            {
                return await _dbSet
                .OrderBy(p => p.PaymentDate)
                .Select(PaymentsMapper.AsPaymentDTO)
                .ToListAsync();
            }
            else
            {
                return await _dbSet
                .OrderByDescending(p => p.PaymentDate)
                .Select(PaymentsMapper.AsPaymentDTO)
                .ToListAsync();
            }
        }

        public async Task<decimal> GetTotalPaidByOrderIdAsync(int orderId)
        {
            return await _dbSet
            .Where(p => p.OrderId == orderId)
            .SumAsync(p => p.Amount ?? 0);
        }
    }
}
