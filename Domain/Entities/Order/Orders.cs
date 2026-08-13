using Domain.Base;
using Domain.Entities.User;
using Domain.Exceptions;

namespace Domain.Entities.Order
{
    public class Orders : BaseEntity<int>
    {
        public int? UserId { get; private set; }
        public DateTime? OrderDate { get; private set; }
        public decimal? TotalAmount { get; private set; }
        public decimal? TotalProfit { get; private set; }
        public int? OrderStatusId { get; private set; } // 0: Pending, 1: Completed, 2: Cancelled
        public string? OrderSource { get; private set; } //max length 20
        public string? DeliveryType { get; private set; } //max length 20
        public string ShippingAddress { get; private set; } //max length 255
        public string CustomerPhone { get; private set; } //max length 20

        //Navegation properties
        public virtual Users User {get; set;}

        private Orders(){}

        private Orders (int? userId, decimal? totalAmount, decimal? totalProfit, string? orderSource, string? deliveryType, string shippingAddress, string customerPhone)
        {
            UserId = userId;
            OrderDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
            TotalProfit = totalProfit;
            OrderStatusId = 0;
            OrderSource = orderSource;
            DeliveryType = deliveryType;
            ShippingAddress = shippingAddress;
            CustomerPhone = customerPhone;
        }

        public static Orders CreateOrder(int? userId, decimal? totalAmount, decimal? totalProfit, string? orderSource, string? deliveryType, string shippingAddress, string customerPhone)
        {
            if(userId <= 0)
                throw new DomainException("El usuario de la orden debe de ser valido");
            if(totalAmount <= 0)
                throw new DomainException("El total de la venta no puede ser menor o igual a 0");
            if(String.IsNullOrWhiteSpace(orderSource))
                throw new DomainException("Se debe agregar el de donde viene la orden");
            if(String.IsNullOrWhiteSpace(deliveryType))
                throw new DomainException("Se debe agregar el tipo de delivery");

            return new Orders(userId, totalAmount, totalProfit, orderSource, deliveryType, shippingAddress ?? String.Empty, customerPhone ?? String.Empty);
        }
    }
}
