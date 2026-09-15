using Domain.Entities.Order.Enums;

namespace Application.DTOs.Order.Responses
{
    public record CreateOrderResponse
    {
        public int OrderId { get; init; }
        public List<CreateOrderItemResponse> Items { get; init; } = new();
        public string UserName { get; init; }
        public DateTimeOffset OrderDate { get; init; }
        public decimal TotalAmount { get; init; }
        public OrderStatuses OrderStatus { get; init; }
        public OrderSources OrderSource { get; init; }
        public DeliveryTypes DeliveryType { get; init; }
        public string? ShippingAddress { get; init; }

    }
}
