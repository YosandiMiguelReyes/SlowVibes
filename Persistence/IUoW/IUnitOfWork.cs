

using Domain.Repositories;
using Application.Contracts.Repositories.InventoryMovement;
using Application.Contracts.Repositories.Order;
using Application.Contracts.Repositories.Payment;
using Application.Contracts.Repositories.Product;
using Application.Contracts.Repositories.Users;
using Persistence.Interfaces.Order;

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
