using Domain.Base;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities.InventoryMovement
{
    public class InventoryMovements : BaseEntity<int>, ICreatedAt
    {
        public int? ProductId { get; private set; }
        public int OrderId { get; private set; }
        public int? Quantity { get; private set; }
        public int? MovementTypeId { get; private set; }
        public int? ReasonId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private InventoryMovements (){}

        private InventoryMovements (int? productId, int orderId, int? quantity, int? movementTypeId, int? reasonId)
        {
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
            MovementTypeId = movementTypeId;
            ReasonId = reasonId;
            CreatedAt = DateTime.UtcNow;
        }

        public static InventoryMovements CreateInventoryMovements (int? productId, int orderId, int? quantity, int? movementTypeId, int? reasonId)
        {
            if(productId <= 0)
                new DomainException("El product Id debe ser de un producto valido");
            if(quantity <= 0)
                new DomainException("Al menos un producto debe ser seleccionado");
            if(movementTypeId <= 0)
                new DomainException("El ID del tipo de movimiento del inventario debe ser valido");
            if(reasonId <= 0)
                new DomainException("El ID del tipo de la rason del inventario debe ser valido");

            return new InventoryMovements(productId, orderId, quantity, movementTypeId, reasonId);
        }

    }
}
