using Domain.Entities.Product;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.DTO.Product;
using Persistence.Interfaces.Product;
using Persistence.Mappers.ProductMappers;


namespace Persistence.Repositories.Product
{
    public class ProductsRepository : BaseRepository<Products, int>, IProductsRepository
    {
       
        public ProductsRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetActiveProductsAsync()
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

        /*public Task<IEnumerable<ProductWithDiscountDTO>> GetInactiveProductsAsync()
        {
            throw new NotImplementedException();
        }*/ // Only admin can see inactive products

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

        /*public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByPurchasePriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }*/ // Only admin can see products by purchase price

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsBySalePriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        /*public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByStockAsync(int stock)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsLowStockAsync()
        {
            throw new NotImplementedException();
        }*/ // I don't think the client needs to see products by stock or low stock.
    }
}
