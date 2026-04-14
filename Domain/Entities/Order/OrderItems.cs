using Domain.Base;

namespace Domain.Entities.Order
{
    public class OrderItems : BaseEntity<int>
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? DiscountApplied { get; set; }
        public decimal? Profit { get; set; }
    }
}
