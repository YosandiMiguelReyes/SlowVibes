using Domain.Base;
using Domain.Interfaces;

namespace Domain.Entities.InventoryMovement
{
    public class InventoryMovements : BaseEntity<int>, ICreatedAt
    {
        public int? ProductId { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }
        public int? MovementTypeId { get; set; }
        public int? ReasonId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
