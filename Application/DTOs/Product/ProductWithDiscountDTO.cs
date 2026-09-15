using System.Text.Json.Serialization;

namespace Application.DTOs.Product;

public class ProductWithDiscountDTO
{
    public string? CategoryName { get; set; }
    public string? Name { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal? SalePrice { get; set; }
    public decimal? SalePriceWithDiscount { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? Percentage { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? StartDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? EndDate { get; set; }
}
