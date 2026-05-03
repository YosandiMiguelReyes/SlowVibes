

using Domain.Entities.Product;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.Product;

namespace Persistence.Repositories.Product
{
    public class ProductDiscountRepository : BaseRepository<ProductDiscounts, int>, IProductDiscountsRepository
    {
        public ProductDiscountRepository(SlowVibesDbContext context) : base(context)
        {
            
        }
        public Task<IEnumerable<ProductDiscounts>> GetActiveProductsWithDiscountsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDiscounts>> GetProductswithDiscountbyEndDateAsync(DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDiscounts>> GetProductswithDiscountbyStartDateAsync(DateTime startDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDiscounts>> GetProductsWithDiscountsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDiscounts>> GetProductsWithDiscountsByPercentageAsync(decimal percentage)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDiscounts>> GetProductWithDiscountByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
