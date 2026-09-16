using System.Text.Json.Serialization;

namespace Application.DTOs.Product;

public class AdminProductWithDiscountDTO
{
    public string? CategoryName { get; set; }
    public int? ProductId { get; set; }
    public int? CategoryId { get; set; }
    public string? ProductName { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal? PurchasePrice { get; set; }
    public decimal? SalePrice { get; set; }
    public int? Stock { get; set; }
    public int? LowStockThreshold { get; set; }
    public bool ProductIsActive { get; set; }
    public DateTime CreatedAt { get; set; }

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
