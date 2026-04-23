using Domain.Entities.Product;
using Persistence.BaseRepository;
using Persistence.Interfaces.Product;


namespace Persistence.Repositories.Product
{
    public class ProductsRepository : BaseRepository<Products, int>, IProductsRepository
    {
        public Task<IEnumerable<Products>> GetActiveProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Products>> GetInactiveProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Products> GetProductBySkuAsync(string skuName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Products>> GetProductsByCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Products>> GetProductsByNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Products>> GetProductsByPurchasePriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Products>> GetProductsBySalePriceAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Products>> GetProductsByStockAsync(int stock)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Products>> GetProductsLowStockAsync()
        {
            throw new NotImplementedException();
        }
    }
}
