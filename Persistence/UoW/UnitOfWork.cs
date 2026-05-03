
using Domain.Entities.InventoryMovement;
using Domain.Repositories;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.InventoryMovement;
using Persistence.Interfaces.Order;
using Persistence.Interfaces.Payment;
using Persistence.Interfaces.Product;
using Persistence.Interfaces.Users;
using Persistence.Repositories.InventoryMovement;
using Persistence.Repositories.Order;
using Persistence.Repositories.Payment;
using Persistence.Repositories.Product;
using Persistence.Repositories.Users;

namespace Persistence.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SlowVibesDbContext _context;
        public UnitOfWork(SlowVibesDbContext context)
        {
            _context = context;
        }


        private IUserRepository _user;
        private IProductsRepository _products;
        private IProductDiscountsRepository _productDiscounts;
        private IOrderRepository _orders;
        private IOrderItemsRepository _orderItems;
        private IPaymentsRepository _payments;
        private IInventoryMovementsRepository _inventoryMovements;



        public IUserRepository Users => _user ??= new UserRepository(_context);

        public IProductsRepository Products => _products ??= new ProductsRepository(_context);

        public IProductDiscountsRepository ProductDiscounts => _productDiscounts ??= new ProductDiscountRepository(_context);

        public IOrderRepository Orders => _orders ??= new OrderRepository(_context);

        public IOrderItemsRepository OrderItems => _orderItems ??= new OrderItemsRepository(_context);

        public IPaymentsRepository Payments => _payments ??= new PaymentsRepository(_context);

        public IInventoryMovementsRepository InventoryMovements => _inventoryMovements ??= new InventoryMovementRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IBaseRepository<TEntity, TId> Repository<TEntity, TId>() where TEntity : class
        {
            return new BaseRepository<TEntity, TId>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
