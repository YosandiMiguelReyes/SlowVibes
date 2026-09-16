using Domain.Base;
using Domain.Exceptions;

namespace Domain.Entities.Order
{
    public class OrderItems : BaseEntity<int>
    {
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }

        public decimal PurchasePrice { get; private set; }
        public decimal SalePrice { get; private set; }
        public decimal DiscountApplied { get; private set; }

        public decimal Profit { get; private set; }

        private OrderItems(){}

        private OrderItems(int productId, int quantity, decimal purchasePrice, decimal salePrice, decimal discountApplied)
        {
            ProductId = productId;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            DiscountApplied = discountApplied;
            Profit = CalculateProfit();
        }

        public static OrderItems CreateOrderItems(int productId, int quantity, decimal purchasePrice, decimal salePrice, decimal discountApplied)
        {
            if(productId <= 0)
                throw new DomainException("El producto debe ser válido.");

            if(quantity <= 0)
                throw new DomainException("La cantidad no puede ser cero o menor");

            if (purchasePrice < 0)
                throw new DomainException("El precio de compra no puede ser negativo.");
            
            if(salePrice < 0)
                throw new DomainException("El precio de venta no puede ser negativo.");

            if(discountApplied < 0 || discountApplied > 90)
                throw new DomainException("El descuento aplicado no puede ser menor que 0% ni mayor al 90%");
            
            return new OrderItems(productId, quantity, purchasePrice, salePrice, discountApplied);
        }

        private decimal CalculateProfit()
        {
            var discountedSalePrice = SalePrice - (SalePrice * DiscountApplied / 100m);

            return (discountedSalePrice - PurchasePrice) * Quantity;
        }

        public void ChangeQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("La cantidad debe ser mayor que cero.");

            Quantity = quantity;

            Profit = CalculateProfit();
        }
    }
}
