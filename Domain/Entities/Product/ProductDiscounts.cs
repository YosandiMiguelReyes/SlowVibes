using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities.Product
{
    public class ProductDiscounts : BaseEntity<int>, IIsActive
    {
        public int? ProductId { get; set; }
        public decimal? Percentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
