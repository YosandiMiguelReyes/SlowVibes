

using Domain.Entities.Product;
using Domain.Repositories;

namespace Persistence.Interfaces.Product
{
    public interface IProductsRepository : IBaseRepository<Products, int>
    {
        Task<IEnumerable<Products>> GetProductsByCategoryAsync(string categoryName);
        Task<IEnumerable<Products>> GetProductsByNameAsync(string productName);
        Task<Products> GetProductBySkuAsync (string skuName);
        Task<IEnumerable<Products>> GetProductsByPurchasePriceAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Products>> GetProductsBySalePriceAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Products>> GetProductsByStockAsync(int stock);
        Task<IEnumerable<Products>> GetProductsLowStockAsync();
        Task<IEnumerable<Products>> GetActiveProductsAsync();
        Task<IEnumerable<Products>> GetInactiveProductsAsync();
    }
}
