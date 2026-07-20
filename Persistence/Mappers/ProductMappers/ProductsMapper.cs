using Domain.Entities.Product;
using Persistence.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;

namespace Persistence.Mappers.ProductMappers
{
    public static class ProductstMapper
    {
        public static Expression<Func<Products, ProductWithDiscountDTO>> AsProductWithDiscount = p => new ProductWithDiscountDTO
        {
            CategoryName = p.Category.Name ?? "Sin categoria",
            Name = p.Name,
            SKU = p.SKU,
            Description = p.Description ?? "Sin Descripción",
            ImageUrl = p.ImageUrl ?? "Default-image.jpg",

            // 2. Si está activo calcula la rebaja; si no, el precio final es el precio normal
            SalePriceWithDiscount = p.ProductDiscounts.IsActive
                    ? (p.SalePrice ?? 0) - ((p.SalePrice ?? 0) * ((p.ProductDiscounts.Percentage ?? 0) / 100m))
                    : p.SalePrice,

            // 3. Si no está activo, van a null para que JsonIgnore los esconda del cliente
            Percentage = p.ProductDiscounts.IsActive ? p.ProductDiscounts.Percentage : null,
            StartDate = p.ProductDiscounts.IsActive ? p.ProductDiscounts.StartDate : null,
            EndDate = p.ProductDiscounts.IsActive ? p.ProductDiscounts.EndDate : null
        };

        public static Expression<Func<Products, AdminProductWithDiscountDTO>> AsAdminProductWithDiscount = p => new AdminProductWithDiscountDTO
        {
            CategoryName = p.Category.Name ?? "Sin categoria",
            CategoryId = p.CategoryId,
            ProductName = p.Name,
            ProductId = p.Id,
            SKU = p.SKU,
            Description = p.Description ?? "Sin Descripcion",
            ImageUrl = p.ImageUrl ?? "Defult-image.jpg",
            PurchasePrice = p.PurchasePrice,
            SalePrice = p.SalePrice,
            DiscountedPrice = p.ProductDiscounts.IsActive

                    ? (p.SalePrice ?? 0) - ((p.SalePrice ?? 0) * ((p.ProductDiscounts.Percentage ?? 0) / 100m))
                    : null,

            Stock = p.Stock,
            LowStockThreshold = p.LowStockThreshold,
            ProductIsActive = p.IsActive,
            CreatedAt = p.CreatedAt,

            DiscountPercentage = p.ProductDiscounts.IsActive ? p.ProductDiscounts.Percentage : null,
            DiscountStartDate = p.ProductDiscounts.IsActive ? p.ProductDiscounts.StartDate : null,
            DiscountEndDate = p.ProductDiscounts.IsActive ? p.ProductDiscounts.EndDate : null,
            DiscountIsActive = p.ProductDiscounts.IsActive ? true : null,

        };
    }
}
