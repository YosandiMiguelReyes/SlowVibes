using Domain.Entities.Order.Enums;

namespace Application.DTOs.Order.Requests
{
    public record CreateOrderRequest
    {
        public int UserId { get; init; }
        public OrderSources OrderSource { get; init; }
        public DeliveryTypes DeliveryType { get; init; }
        public string? ShippingAddress { get; init; }

        public List<CreateOrderItemRequest> Items { get; init; } = new();
    }
}
