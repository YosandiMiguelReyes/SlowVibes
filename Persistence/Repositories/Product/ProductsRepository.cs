using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Persistence.BaseRepository;
using Persistence.Context;
using Application.DTOs.Product;
using Application.Contracts.Repositories.Product;
using Persistence.Mappers.ProductMappers;


namespace Persistence.Repositories.Product
{
    public class ProductsRepository : BaseRepository<Products, int>, IProductsRepository
    {
       
        public ProductsRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetActiveProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetInactiveProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AdminProductWithDiscountDTO> AdminGetProductBySkuAsync(string skuName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByPurchasePriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsBySalePriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByStockAsync(int stock)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsLowStockAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetActiveProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductWithDiscountDTO> GetProductBySkuAsync(string skuName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsBySalePriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }
    }
}
