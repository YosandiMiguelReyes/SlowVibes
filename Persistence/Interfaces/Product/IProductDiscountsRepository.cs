

using Domain.Entities.Product;
using Domain.Repositories;

namespace Persistence.Interfaces.Product
{
    public interface IProductDiscountsRepository : IBaseRepository<ProductDiscounts, int>
    {
        Task<IEnumerable<ProductDiscounts>> GetProductWithDiscountByNameAsync(string name);
        Task<IEnumerable<ProductDiscounts>> GetProductsWithDiscountsByPercentageAsync(decimal percentage);
        Task<IEnumerable<ProductDiscounts>> GetActiveProductsWithDiscountsAsync();
        Task<IEnumerable<ProductDiscounts>> GetProductswithDiscountbyStartDateAsync(DateTime startDate);
        Task<IEnumerable<ProductDiscounts>> GetProductswithDiscountbyEndDateAsync(DateTime endDate);
        Task<IEnumerable<ProductDiscounts>> GetProductsWithDiscountsByDateRangeAsync(DateTime startDate, DateTime endDate);

    }
}
