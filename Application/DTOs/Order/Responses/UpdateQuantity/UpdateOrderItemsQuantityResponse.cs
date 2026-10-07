using Domain.Entities.Order.Enums;

namespace Application.DTOs.Order.Responses.UpdateQuantity;

    public record UpdateOrderItemsQuantityResponse
    {
        public int OrderId { get; init; }
        public DateTimeOffset OrderDate { get; init; }
        public decimal TotalAmount { get; init; }
        public OrderStatuses OrderStatus { get; init; }
        public OrderSources OrderSource { get; init; }
        public DeliveryTypes DeliveryType { get; init; }
        public string? ShippingAddress { get; init; }
        public List<UpdateOrderItemQuantityResponse> Items { get; init; } = new();
    }
