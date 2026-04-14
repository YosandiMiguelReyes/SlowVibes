using Domain.Base;

namespace Domain.Entities.InventoryMovement
{
    public class InventoryMovementReasons : BaseEntity<int>
    {
        public string? Name { get; set; } //max length 50
    }
}
