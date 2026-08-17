using Domain.Base;
using Domain.Exceptions;

namespace Domain.Entities.Order
{
    public class OrderItems : BaseEntity<int>
    {
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal DiscountApplied { get; private set; } = 0;
        public decimal Profit { get; private set; }

        private OrderItems(){}

        private OrderItems(int orderId, int productId, int quantity, decimal unitPrice, decimal discountApplied, decimal profit )
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountApplied = discountApplied;
            Profit = profit;
        }

        public static OrderItems CreateOrderItems(int orderId, int productId, int quantity, decimal unitPrice, decimal discountApplied, decimal profit )
        {
            if(orderId <= 0)
                throw new DomainException("La orden debe de ser valida");

            if(productId <= 0)
                throw new DomainException("El producto debe de ser valido");

            if(quantity <= 0)
                throw new DomainException("La cantidad no puede ser cero o menor");

            if(discountApplied < 0 || discountApplied > 100)
                throw new DomainException("El descuento aplicado no puede ser menor que 0% ni mayor al 100%");
            
            return new OrderItems(orderId, productId, quantity, unitPrice, discountApplied, profit);
        }
    }
}
