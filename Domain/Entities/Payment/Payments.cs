using Domain.Base;

namespace Domain.Entities.Payment
{
    public class Payments : BaseEntity<int>
    {
        public int? OrderId { get; set; }
        public int? PaymentMethodId { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; } //max length 20
        public string? ReferenceNumber { get; set; } //max length 100
        public DateTime? PaymentDate { get; set; }
    }
}
