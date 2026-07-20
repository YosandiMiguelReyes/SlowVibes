

using System.Text.Json.Serialization;

namespace Persistence.DTO.Product
{
    public class ProductWithDiscountDTO
    {
        public string? CategoryName { get; set; }
        public string? Name { get; set; } //max length 150
        public string SKU { get; set; } //max length 50
        public string Description { get; set; }
        public string ImageUrl { get; set; } //max length 500
        public decimal? SalePrice { get; set; }
        public decimal? SalePriceWithDiscount { get; set; }

        //Discount
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Percentage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? StartDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? EndDate { get; set; }
    }
}
