using Domain.Base;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities.InventoryMovement
{
    public class InventoryMovements : BaseEntity<int>, ICreatedAt
    {
        public int ProductId { get; private set; }
        public int? OrderId { get; private set; }
        public int Quantity { get; private set; }
        public int MovementTypeId { get; private set; }
        public int ReasonId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        private InventoryMovements (){}

        private InventoryMovements (int productId, int? orderId, int quantity, int movementTypeId, int reasonId)
        {
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
            MovementTypeId = movementTypeId;
            ReasonId = reasonId;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static InventoryMovements Create (int productId, int? orderId, int quantity, int movementTypeId, int reasonId)
        {
            if(productId <= 0)
                throw new DomainException("El ProductId debe ser de un producto válido");
            if(quantity <= 0)
                throw new DomainException("Al menos un producto debe ser seleccionado");
            if(movementTypeId <= 0)
                throw new DomainException("El ID del tipo de movimiento debe ser válido.");
            if(reasonId <= 0)
                throw new DomainException("El ID del tipo de la razón del inventario debe ser válido.");

            return new InventoryMovements(productId, orderId, quantity, movementTypeId, reasonId);
        }

    }
}
