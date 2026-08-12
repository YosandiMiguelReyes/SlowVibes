using Domain.Base;
using Domain.Exceptions;

namespace Domain.Entities.Order
{
    public class OrderItems : BaseEntity<int>
    {
        public int? OrderId { get; private set; }
        public int? ProductId { get; private set; }
        public int? Quantity { get; private set; }
        public decimal? UnitPrice { get; private set; }
        public decimal? DiscountApplied { get; private set; }
        public decimal? Profit { get; private set; }

        private OrderItems(){}

        private OrderItems(int? orderId, int? productId, int? quantity, decimal? unitPrice, decimal? discountApplied, decimal? profit )
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountApplied = discountApplied;
            Profit = profit;
        }

        public static OrderItems CreateOrderItems(int? orderId, int? productId, int? quantity, decimal? unitPrice, decimal? discountApplied, decimal? profit )
        {
            if(orderId <= 0)
                new DomainException("La orden debe de ser valida");

            if(productId <= 0)
                new DomainException("El producto debe de ser valido");

            if(quantity <= 0)
                new DomainException("La cantidad no puede ser cero o menor");

            
            
            return new OrderItems(orderId, productId, quantity, unitPrice, discountApplied, profit);
        }
    }
}
