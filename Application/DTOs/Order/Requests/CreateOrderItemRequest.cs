

namespace Application.DTOs.Order.Requests
{
    public record CreateOrderItemRequest
    {
        public int ProductId { get; init; }
        public int Quantity { get; init; }
    }
}
