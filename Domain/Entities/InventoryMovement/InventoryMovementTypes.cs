using Domain.Base;

namespace Domain.Entities.InventoryMovement
{
    public class InventoryMovementTypes : BaseEntity<int>
    {
        public string? Name { get; set; } //max length 20
    }
}
