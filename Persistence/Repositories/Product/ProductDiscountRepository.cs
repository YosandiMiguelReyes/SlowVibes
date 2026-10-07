

using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.BaseRepository;
using Persistence.Context;
using Application.DTOs.Product;
using Application.Contracts.Repositories.Product;
//using Persistence.Mappers.ProductMappers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Persistence.Repositories.Product
{
    public class ProductDiscountRepository : BaseRepository<ProductDiscounts, int>, IProductDiscountsRepository
    {
        public ProductDiscountRepository(SlowVibesDbContext context) : base(context)
        {
            
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductswithDiscountbyEndDateAsync(DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductswithDiscountbyStartDateAsync(DateTime startDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductsWithDiscountsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductsWithDiscountsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductsWithDiscountsByPercentageAsync(decimal percentage)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductWithDiscountByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetActiveProductsWithDiscountsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductswithDiscountbyEndDateAsync(DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductswithDiscountbyStartDateAsync(DateTime startDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsWithDiscountsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductsWithDiscountsByPercentageAsync(decimal percentage)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithDiscountDTO>> GetProductWithDiscountByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
