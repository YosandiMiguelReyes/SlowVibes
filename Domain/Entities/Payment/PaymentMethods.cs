using Domain.Base;

namespace Domain.Entities.Payment
{
    public class PaymentMethods : BaseEntity<int>
    {
        public string? Name { get; set; } //max length 20. 0 transferencia, 1 efectivo, 2 tarjeta
    }
}
