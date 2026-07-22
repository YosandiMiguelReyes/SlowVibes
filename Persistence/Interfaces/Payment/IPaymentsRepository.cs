

using Domain.Entities.Payment;
using Domain.Repositories;
using Persistence.DTO.Payment;

namespace Persistence.Interfaces.Payment
{
    public interface IPaymentsRepository : IBaseRepository<Payments, int>
    {
        // all queries responses must have order and user
        Task<IEnumerable<PaymentDTO>> GetPaymentsByUserNameAsync(string userName);
        Task<PaymentDTO?> GetPaymentsByOrderIdAsync(int orderId);
        Task<IEnumerable<PaymentDTO>> GetPaymentsByMethodAsync(string method);
        Task<IEnumerable<PaymentDTO>> GetPaymentsByAmountRangeAsync(decimal minAmount, decimal maxAmount);
        Task<IEnumerable<PaymentDTO>> GetPaymentsByStatusAsync(string status);
        Task<PaymentDTO?> GetPaymentsByReferenceNumberAsync(string referenceNumber);
        Task<IEnumerable<PaymentDTO>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<PaymentDTO>> GetPaymentsOrderedByAmountAsync(bool ascending);
        Task<IEnumerable<PaymentDTO>> GetPaymentsOrderedByDateAsync(bool ascending);
        Task<decimal> GetTotalPaidByOrderIdAsync(int orderId);
        Task<IEnumerable<PaymentDTO>> GetPaymentByUserId(int userId);

    }
}
