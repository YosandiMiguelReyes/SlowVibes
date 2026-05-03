using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities.Product
{
    public class Products : BaseEntity<int>, IIsActive, ICreatedAt
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; } //max length 150
        public string SKU { get; set; } //max length 50
        public string Description { get; set; }
        public string ImageUrl { get; set; } //max length 500
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public int? Stock { get; set; }
        public int? LowStockThreshold { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
