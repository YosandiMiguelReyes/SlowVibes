

using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.DTO.Product;
using Persistence.Interfaces.Product;
using Persistence.Mappers.ProductMappers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Persistence.Repositories.Product
{
    public class ProductDiscountRepository : BaseRepository<ProductDiscounts, int>, IProductDiscountsRepository
    {
        public ProductDiscountRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductsWithDiscountsAsync()
        {
            return await _dbSet
                .Where(pd => pd.IsActive)
                .Select(Pd => Pd.Product)
                .Select(ProductstMapper.AsAdminProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductswithDiscountbyEndDateAsync(DateTime endDate)
        {
            return await _dbSet
                .Where(pd => pd.IsActive && pd.EndDate <= endDate.Date)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsAdminProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductswithDiscountbyStartDateAsync(DateTime startDate)
        {
            return await _dbSet
                .Where(pd => pd.IsActive && pd.StartDate >= startDate.Date)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsAdminProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductsWithDiscountsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(pd => pd.IsActive && pd.StartDate >= startDate.Date 
                        && pd.EndDate <= endDate.Date)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsAdminProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductsWithDiscountsByPercentageAsync(decimal percentage)
        {
            return await _dbSet
                .Where(pd => pd.IsActive && pd.Percentage.HasValue 
                        && pd.Percentage == percentage)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsAdminProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductWithDiscountByNameAsync(string name)
        {
            return await _dbSet
                .Where(pd => pd.IsActive && pd.Product.Name.Contains(name))
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsAdminProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetActiveProductsWithDiscountsAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _dbSet
                .Where(pd => pd.IsActive
                && pd.StartDate <= today
                && pd.EndDate >= today
                && pd.Product.IsActive == true)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetProductswithDiscountbyEndDateAsync(DateTime endDate)
        {
            return await _dbSet
                .Where(pd => pd.EndDate <= endDate 
                && pd.IsActive == true
                && pd.EndDate >= DateTime.Today
                && pd.Product.IsActive == true)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetProductswithDiscountbyStartDateAsync(DateTime startDate)
        {
            return await _dbSet
                .Where(pd => pd.StartDate >= startDate 
                && pd.IsActive == true
                && pd.Product.IsActive == true)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetProductsWithDiscountsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(pd => pd.StartDate >= startDate 
                && pd.EndDate <= endDate 
                && pd.IsActive == true
                && pd.Product.IsActive == true)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetProductsWithDiscountsByPercentageAsync(decimal percentage)
        {
            return await _dbSet
                .Where(pd => pd.Percentage.HasValue 
                && pd.Percentage == percentage 
                && pd.IsActive == true
                && pd.Product.IsActive == true)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsProductWithDiscount).ToListAsync();
        }

        public async Task<IEnumerable<ProductWithDiscountDTO>> GetProductWithDiscountByNameAsync(string name)
        {
            return await _dbSet
                .Where(pd => pd.Product.Name.Contains(name) 
                && pd.IsActive == true
                && pd.Product.IsActive == true)
                .Select(pd => pd.Product)
                .Select(ProductstMapper.AsProductWithDiscount).ToListAsync();
        }
    }
}
