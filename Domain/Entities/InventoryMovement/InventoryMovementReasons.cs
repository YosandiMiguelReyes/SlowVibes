using Domain.Base;
using Domain.Exceptions;

namespace Domain.Entities.InventoryMovement
{
    public class InventoryMovementReasons : BaseEntity<int>
    {
        public string Name { get; private set; } = string.Empty;

        private InventoryMovementReasons(){}
        private InventoryMovementReasons(string name)
        {
            Name = name;
        }

        public static InventoryMovementReasons Create(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre de la razón del inventario debe de ser válida");

            return new InventoryMovementReasons(name.Trim());
        }

        public void Update(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre de la razón del inventario debe de ser válida");

            Name = name.Trim();
        }
    }
}
