

using Domain.Entities.Product;
using Domain.Repositories;
using Application.DTOs.Product;

namespace Application.Contracts.Repositories.Product
{
    public interface IProductDiscountsRepository : IBaseRepository<ProductDiscounts, int>
    {
        Task<IEnumerable<ProductWithDiscountDTO>> GetProductWithDiscountByNameAsync(string name);
        Task<IEnumerable<ProductWithDiscountDTO>> GetProductsWithDiscountsByPercentageAsync(decimal percentage);
        Task<IEnumerable<ProductWithDiscountDTO>> GetActiveProductsWithDiscountsAsync();
        Task<IEnumerable<ProductWithDiscountDTO>> GetProductswithDiscountbyStartDateAsync(DateTime startDate);
        Task<IEnumerable<ProductWithDiscountDTO>> GetProductswithDiscountbyEndDateAsync(DateTime endDate);
        Task<IEnumerable<ProductWithDiscountDTO>> GetProductsWithDiscountsByDateRangeAsync(DateTime startDate, DateTime endDate);

        //admin
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductWithDiscountByNameAsync(string name);
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductsWithDiscountsByPercentageAsync(decimal percentage);
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductsWithDiscountsAsync();
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductswithDiscountbyStartDateAsync(DateTime startDate);
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductswithDiscountbyEndDateAsync(DateTime endDate);
        Task<IEnumerable<AdminProductWithDiscountDTO>> AdminGetAllProductsWithDiscountsByDateRangeAsync(DateTime startDate, DateTime endDate);

    }
}
