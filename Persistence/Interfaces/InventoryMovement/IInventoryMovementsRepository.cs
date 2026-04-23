

using Domain.Entities.InventoryMovement;
using Domain.Repositories;

namespace Persistence.Interfaces.InventoryMovement
{
    public interface IInventoryMovementsRepository : IBaseRepository<InventoryMovements, int>
    {
        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByReasonIdAsync(int reasonId);
        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByProductIdAsync(int productId);
        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByOrderIdAsync(int orderId);

        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByUserNameAsync(string userName);
        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByProductNameAsync(string productName);
        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByOrderSourceAsync(string orderSource);

        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByMovementTypeAsync(string movementType);
        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByReasonAsync(string reason);
        
        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsOrderedByDateAsync(bool ascending);
        Task<IEnumerable<InventoryMovements>> GetInventoryMovementsOrderedByQuantityAsync(bool ascending);
        
    }
}
