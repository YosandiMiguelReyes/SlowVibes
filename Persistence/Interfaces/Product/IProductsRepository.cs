

using Domain.Entities.Product;
using Domain.Repositories;
using Persistence.DTO.Product;

namespace Persistence.Interfaces.Product
{
    public interface IProductsRepository : IBaseRepository<Products, int>
    {
        Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByCategoryAsync(string categoryName);
        Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByNameAsync(string productName);
        Task<ProductWithDiscountDTO> GetProductBySkuAsync (string skuName);
        //Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByPurchasePriceAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<ProductWithDiscountDTO>> GetProductsBySalePriceAsync(decimal minPrice, decimal maxPrice);
        //Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByStockAsync(int stock);
        //Task<IEnumerable<ProductWithDiscountDTO>> GetProductsLowStockAsync();
        Task<IEnumerable<ProductWithDiscountDTO>> GetActiveProductsAsync();
        //Task<IEnumerable<ProductWithDiscountDTO>> GetInactiveProductsAsync();

        //Admin Only
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByCategoryAsync(string categoryName);
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByNameAsync(string productName);
        Task<AdminProductWithDiscountDTO> AdminGetProductBySkuAsync(string skuName);
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByPurchasePriceAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsBySalePriceAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByStockAsync(int stock);
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsLowStockAsync();
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetActiveProductsAsync();
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetInactiveProductsAsync();
    }
}
