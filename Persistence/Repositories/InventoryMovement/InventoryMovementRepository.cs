
using Domain.Entities.InventoryMovement;
using Persistence.BaseRepository;
using Persistence.Context;
using Persistence.Interfaces.InventoryMovement;

namespace Persistence.Repositories.InventoryMovement
{
    internal class InventoryMovementRepository : BaseRepository<InventoryMovements, int>, IInventoryMovementsRepository
    {
        public InventoryMovementRepository(SlowVibesDbContext context) : base(context)
        {
            
        }
        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByMovementTypeAsync(string movementType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByOrderIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByOrderSourceAsync(string orderSource)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByProductNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByReasonAsync(string reason)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByReasonIdAsync(int reasonId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsOrderedByDateAsync(bool ascending)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InventoryMovements>> GetInventoryMovementsOrderedByQuantityAsync(bool ascending)
        {
            throw new NotImplementedException();
        }
    }
}
