using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Persistence.DTO.Product
{
    public class AdminProductWithDiscountDTO
    {
        //product
        public string? CategoryName { get; set; }
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string? ProductName { get; set; } //max length 150
        public string SKU { get; set; } //max length 50
        public string Description { get; set; }
        public string ImageUrl { get; set; } //max length 500
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public int? Stock { get; set; }
        public int? LowStockThreshold { get; set; }
        public bool ProductIsActive { get; set; }
        public DateTime CreatedAt { get; set; }


        //Discounts
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? DiscountedPrice { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? DiscountPercentage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DiscountStartDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DiscountEndDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? DiscountIsActive { get; set; }
    }
}
