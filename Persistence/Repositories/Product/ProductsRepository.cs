using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
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
            return await _dbSet
            .Where(p => p.IsActive == true)
            .Select(ProductstMapper.AsAdminProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetInactiveProductsAsync()
        {
            return await _dbSet
            .Where(p => p.IsActive == false)
            .Select(ProductstMapper.AsAdminProductWithDiscount).ToListAsync();
        }

        public async Task<AdminProductWithDiscountDTO> AdminGetProductBySkuAsync(string skuName)
        {
            return await _dbSet
            .Where(p => p.SKU == skuName)
            .Select(ProductstMapper.AsAdminProductWithDiscount)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByCategoryAsync(string categoryName)
        {
            return await _dbSet
            .Where(p => p.Category.Name.Contains(categoryName))
            .Select(ProductstMapper.AsAdminProductWithDiscount)
            .ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByNameAsync(string productName)
        {
            return await _dbSet
            .Where(p => p.Name.Contains(productName))
            .Select(ProductstMapper.AsAdminProductWithDiscount)
            .ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByPurchasePriceAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet
            .Where(p => p.PurchasePrice >= minPrice && p.PurchasePrice <= maxPrice)
            .Select(ProductstMapper.AsAdminProductWithDiscount)
            .ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsBySalePriceAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet
            .Where(p => p.SalePrice >= minPrice && p.SalePrice <= maxPrice)
            .Select(ProductstMapper.AsAdminProductWithDiscount)
            .ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsByStockAsync(int stock)
        {
            return await _dbSet
            .Where(p => p.Stock == stock)
            .Select(ProductstMapper.AsAdminProductWithDiscount)
            .ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetProductsLowStockAsync()
        {
            return await _dbSet
            .Where(p => p.Stock <= p.LowStockThreshold
                    && p.IsActive == true)
            .Select(ProductstMapper.AsAdminProductWithDiscount)
            .ToListAsync();
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetActiveProductsAsync()
        {
            return await _dbSet
            .Where(p => p.IsActive)
            .Select(ProductstMapper.AsProductWithDiscount)
            .ToListAsync();
        }

        /*public Task<IEnumerable<ProductWithDiscountDTO>> GetInactiveProductsAsync()
        {
            throw new NotImplementedException();
        }*/ // Only admin can see inactive products

        public async Task<ProductWithDiscountDTO> GetProductBySkuAsync(string skuName)
        {
            return await _dbSet
            .Where(p => p.IsActive && p.SKU.Contains(skuName))
            .Select(ProductstMapper.AsProductWithDiscount)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByCategoryAsync(string categoryName)
        {
            return await _dbSet
            .Where(p => p.IsActive && p.Category.Name.Contains(categoryName))
            .Select(ProductstMapper.AsProductWithDiscount)
            .ToListAsync();
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByNameAsync(string productName)
        {
            return await _dbSet
            .Where(p => p.IsActive && p.Name.Contains(productName))
            .Select(ProductstMapper.AsProductWithDiscount)
            .ToListAsync();
        }

        /*public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsByPurchasePriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }*/ // Only admin can see products by purchase price

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetProductsBySalePriceAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet
            .Where(p => p.IsActive && p.SalePrice >= minPrice && p.SalePrice <= maxPrice)
            .Select(ProductstMapper.AsProductWithDiscount)
            .ToListAsync();
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
