using Domain.Base;

namespace Domain.Entities.Order
{
    public class Orders : BaseEntity<int>
    {
        public int? UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalProfit { get; set; }
        public int? OrderStatusId { get; set; } // 0: Pending, 1: Completed, 2: Cancelled
        public string? OrderSource { get; set; } //max length 20
        public string? DeliveryType { get; set; } //max length 20
        public string ShippingAddress { get; set; } //max length 255
        public string CustomerPhone { get; set; } //max length 20
    }
}
