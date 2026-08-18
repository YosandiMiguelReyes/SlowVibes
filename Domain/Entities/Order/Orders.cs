using Domain.Base;
using Domain.Entities.User;
using Domain.Exceptions;
using Domain.Entities.Order.Enums;

namespace Domain.Entities.Order
{
    public class Orders : BaseEntity<int>
    {
        private readonly List<OrderItems> _items = new();

        public IReadOnlyCollection<OrderItems> Items => _items.AsReadOnly();

        public int UserId { get; private set; }
        public DateTimeOffset OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal TotalProfit { get; private set; }
        public OrderStatuses OrderStatus { get; private set; }
        public OrderSources OrderSource { get; private set; } //max length 20
        public DeliveryTypes DeliveryType { get; private set; } //max length 20
        public string? ShippingAddress { get; private set; } //max length 255

        private Orders(){}

        private Orders (int userId, OrderSources orderSource, DeliveryTypes deliveryType, string? shippingAddress)
        {
            UserId = userId;
            OrderDate = DateTimeOffset.UtcNow;
            OrderStatus = OrderStatuses.Pending;

            OrderSource = orderSource;
            DeliveryType = deliveryType;
            ShippingAddress = shippingAddress;

            TotalAmount = 0m;
            TotalProfit = 0m;
        }

        public static Orders Create(int userId, OrderSources orderSource, DeliveryTypes deliveryType, string? shippingAddress)
        {
            if(userId <= 0)
                throw new DomainException("El usuario de la orden debe de ser valido");
            if (deliveryType == DeliveryTypes.Delivery &&
                string.IsNullOrWhiteSpace(shippingAddress))
            {
                throw new DomainException(
                    "Las órdenes de delivery requieren una dirección.");
            }
            if (deliveryType == DeliveryTypes.PickUp)
                shippingAddress = null;
            else
                shippingAddress = shippingAddress!.Trim();

            return new Orders(userId, orderSource, deliveryType, shippingAddress);
        }


                public void AddItem(OrderItems item)
        {
            if (OrderStatus != OrderStatuses.Pending)
                throw new DomainException(
                    "Solo se pueden agregar productos a una orden pendiente.");

            if (item is null)
                throw new DomainException(
                    "El artículo de la orden es obligatorio.");

            var existingItem = _items.FirstOrDefault(
                x => x.ProductId == item.ProductId);

            if (existingItem is not null)
            {
                existingItem.ChangeQuantity(
                    existingItem.Quantity + item.Quantity);
            }
            else
            {
                _items.Add(item);
            }

            RecalculateTotals();
        }

        public void RemoveItem(int productId)
        {
            if (OrderStatus != OrderStatuses.Pending)
                throw new DomainException(
                    "Solo se pueden eliminar productos de una orden pendiente.");

            var item = _items.FirstOrDefault(
                x => x.ProductId == productId);

            if (item is null)
                throw new DomainException(
                    "El producto no pertenece a la orden.");

            _items.Remove(item);

            RecalculateTotals();
        }

        public void Complete()
        {
            if (OrderStatus != OrderStatuses.Pending)
                throw new DomainException(
                    "Solo una orden pendiente puede completarse.");

            if (_items.Count == 0)
                throw new DomainException(
                    "No se puede completar una orden sin productos.");

            if (TotalAmount <= 0)
                throw new DomainException(
                    "La orden debe tener un total mayor que cero.");

            OrderStatus = OrderStatuses.Completed;
        }

        public void Cancel()
        {
            if (OrderStatus == OrderStatuses.Cancelled)
                throw new DomainException(
                    "La orden ya está cancelada.");

            if (OrderStatus == OrderStatuses.Completed)
                throw new DomainException(
                    "Una orden completada no puede ser cancelada.");

            OrderStatus = OrderStatuses.Cancelled;
        }

        private void RecalculateTotals()
        {
            TotalAmount = _items.Sum(item =>
                item.UnitPrice *
                item.Quantity *
                (1m - item.DiscountApplied / 100m));

            TotalProfit = _items.Sum(item => item.Profit);
        }
    }
}
