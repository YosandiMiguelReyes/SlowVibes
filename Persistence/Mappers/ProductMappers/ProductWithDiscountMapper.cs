using Domain.Entities.Product;
using Persistence.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;

namespace Persistence.Mappers.ProductMappers
{
    public static class ProductWithDiscountMapper
    {
        public static Expression<Func<ProductDiscounts, ProductWithDiscountDTO>> AsProductWithDiscount = pd => new ProductWithDiscountDTO
        {
            CategoryName = pd.Product.Category.Name ?? "Sin categoria",
            Name = pd.Product.Name,
            SKU = pd.Product.SKU,
            Description = pd.Product.Description ?? "Sin Descripción",
            ImageUrl = pd.Product.ImageUrl ?? "Default-image.jpg",

            // 2. Si está activo calcula la rebaja; si no, el precio final es el precio normal
            SalePriceWithDiscount = pd.IsActive
                    ? (pd.Product.SalePrice ?? 0) - ((pd.Product.SalePrice ?? 0) * ((pd.Percentage ?? 0) / 100m))
                    : pd.Product.SalePrice,

            // 3. Si no está activo, van a null para que JsonIgnore los esconda del cliente
            Percentage = pd.IsActive ? pd.Percentage : null,
            StartDate = pd.IsActive ? pd.StartDate : null,
            EndDate = pd.IsActive ? pd.EndDate : null
        };

        public static Expression<Func<ProductDiscounts, AdminProductWithDiscountDTO>> AsAdminProductWithDiscount = pd => new AdminProductWithDiscountDTO
        {
            CategoryName = pd.Product.Category.Name ?? "Sin categoria",
            CategoryId = pd.Product.CategoryId,
            ProductName = pd.Product.Name,
            ProductId = pd.ProductId,
            SKU = pd.Product.SKU,
            Description = pd.Product.Description ?? "Sin Descripcion",
            ImageUrl = pd.Product.ImageUrl ?? "Defult-image.jpg",
            PurchasePrice = pd.Product.PurchasePrice,
            SalePrice = pd.Product.SalePrice,
            DiscountedPrice = pd.IsActive

                    ? (pd.Product.SalePrice ?? 0) - ((pd.Product.SalePrice ?? 0) * ((pd.Percentage ?? 0) / 100m))
                    : null,

            Stock = pd.Product.Stock,
            LowStockThreshold = pd.Product.LowStockThreshold,
            ProductIsActive = pd.Product.IsActive,
            CreatedAt = pd.Product.CreatedAt,

            DiscountPercentage = pd.IsActive ? pd.Percentage : null,
            DiscountStartDate = pd.IsActive ? pd.StartDate : null,
            DiscountEndDate = pd.IsActive ? pd.EndDate : null,
            DiscountIsActive = pd.IsActive ? true : null,

        };
    }
}
