

using Domain.Repositories;
using Persistence.Interfaces.InventoryMovement;
using Persistence.Interfaces.Order;
using Persistence.Interfaces.Payment;
using Persistence.Interfaces.Product;
using Persistence.Interfaces.Users;
using Persistence.Repositories.Order;

namespace Persistence.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProductsRepository Products { get; }
        IProductDiscountsRepository ProductDiscounts { get; }
        IOrderRepository Orders { get; }
        IOrderItemsRepository OrderItems { get; }
        IPaymentsRepository Payments { get; }
        IInventoryMovementsRepository InventoryMovements { get; }

        IBaseRepository<T, TId> Repository<T, TId>() where T : class;

        Task<int> SaveChangesAsync();
    }
}
