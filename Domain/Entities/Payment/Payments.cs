using Domain.Base;
using Domain.Entities.Order;
using Domain.Entities.Payment.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Payment
{
    public class Payments : BaseEntity<int>
    {
        public int? OrderId { get; private set; }
        public PaymentMethods PaymentMethod { get; private set; }
        public decimal? Amount { get; private set; }
        public string? Status { get; private set; } //max length 20
        public string? ReferenceNumber { get; private set; } //max length 100
        public DateTime? PaymentDate { get; private set; }

        //Navegation properties
        public virtual Orders order {get; set;}
        public virtual PaymentMethods PaymentMethod {get; set;}

        private Payments (){}

        private Payments (int? orderId, PaymentMethods paymentMethod, decimal? amount, string? status, string referenceNumber)
        {
            OrderId = orderId;
            PaymentMethod = paymentMethod;
            Amount = amount;
            Status = status;
            ReferenceNumber = referenceNumber;
            PaymentDate = DateTime.UtcNow;
        }

        public static Payments CreatePayment(int? orderId, int? paymentMethodId, decimal? amount, string? status, string referenceNumber)
        {
            if(orderId <= 0)
                throw new DomainException("La orden debe de ser valida");
            if(paymentMethodId < 0 || paymentMethodId > 2) 
                throw new DomainException("El metodo de pago debe de ser valido");
            if(amount <= 0)
                throw new DomainException("La cantidad a pagar no puede ser 0 o negativa");
            if(String.IsNullOrWhiteSpace(status))
                throw new DomainException("El pago debe de tener un estado");
            if(String.IsNullOrWhiteSpace(referenceNumber))
                throw new DomainException("El pago debe de tener un numero de referencia");

            return new Payments(orderId, paymentMethodId, amount, status, referenceNumber);
        }
    }
}
