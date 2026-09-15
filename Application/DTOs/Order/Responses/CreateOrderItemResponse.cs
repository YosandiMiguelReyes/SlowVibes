namespace Application.DTOs.Order.Responses
{
    public record CreateOrderItemResponse
    {
        public int OrderId { get; init; }
        public string ProductName { get; init; }
        public int ProductId { get; init; }
        public decimal SubTotal { get; init; }
        public int Quantity { get; init; }
        public decimal SalePrice { get; init; }
        public decimal DiscountApplied { get; init; }
    }
}
