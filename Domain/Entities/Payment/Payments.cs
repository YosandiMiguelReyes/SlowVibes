using Domain.Base;
using Domain.Entities.Payment.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Payment
{
    public class Payments : BaseEntity<int>
    {
        public int OrderId { get; private set; }
        public PaymentMethods PaymentMethod { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; } 
        public string? ReferenceNumber { get; private set; }
        public DateTimeOffset PaymentDate { get; private set; }

        private Payments (){}

        private Payments (int orderId, PaymentMethods paymentMethod, decimal amount, string referenceNumber)
        {
            OrderId = orderId;
            PaymentMethod = paymentMethod;
            Amount = amount;
            Status = PaymentStatus.Pending;
            ReferenceNumber = referenceNumber;
            PaymentDate = DateTimeOffset.UtcNow;
        }

        public static Payments Create(int orderId, PaymentMethods paymentMethod, decimal amount, string? referenceNumber)
        {
            if(orderId <= 0)
                throw new DomainException("La orden debe ser válida.");
            if(amount <= 0)
                throw new DomainException("La cantidad a pagar debe ser mayor que cero.");

            referenceNumber = referenceNumber?.Trim();

            if(paymentMethod == PaymentMethods.Efectivo)
            {
                referenceNumber = null;
            }
            else if(String.IsNullOrWhiteSpace(referenceNumber))
                throw new DomainException("El pago debe tener un número de referencia");

            return new Payments(orderId, paymentMethod, amount, referenceNumber);
        }

        public void MarkAsCompleted()
        {
            EnsurePending();

            Status = PaymentStatus.Completed;
        }

        public void MarkAsFailed()
        {
            EnsurePending();

            Status = PaymentStatus.Failed;
        }

        private void EnsurePending()
        {
            if (Status != PaymentStatus.Pending)
                throw new DomainException("Solo se pueden modificar pagos pendientes.");
        }
    }
}
