using Domain.Entities.Order.Enums;

namespace Application.DTOs.Order.Responses.RemoveOrderItem
{
    public record RemoveOrderItemsReponse
    {
        public int OrderId { get; init; }
        public DateTimeOffset OrderDate { get; init; }
        public decimal TotalAmount { get; init; }
        public OrderStatuses OrderStatus { get; init; }
        public OrderSources OrderSource { get; init; }
        public DeliveryTypes DeliveryType { get; init; }
        public string? ShippingAddress { get; init; }
        public List<RemoveOrderItemReponse> Items { get; init; } = new();
    }
}