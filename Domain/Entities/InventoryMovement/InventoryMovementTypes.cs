using Domain.Base;
using Domain.Exceptions;

namespace Domain.Entities.InventoryMovement
{
    public class InventoryMovementTypes : BaseEntity<int>
    {
        public string Name { get; private set; } = string.Empty;

        private InventoryMovementTypes(){}

        private InventoryMovementTypes (string name)
        {
            Name = name;
        }

        public static InventoryMovementTypes Create(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre del tipo de movimiento de inventario debe ser válido");
            return new InventoryMovementTypes(name.Trim());
        }

        public void Update(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre del tipo de movimiento de inventario debe ser válido");

            Name = name.Trim();
        }
    }
}
