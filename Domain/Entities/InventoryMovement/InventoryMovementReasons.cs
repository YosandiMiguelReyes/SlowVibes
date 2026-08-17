using Domain.Base;
using Domain.Exceptions;

namespace Domain.Entities.InventoryMovement
{
    public class InventoryMovementReasons : BaseEntity<int>
    {
        public string Name { get; private set; } = string.Empty; //max length 50

        private InventoryMovementReasons(){}
        private InventoryMovementReasons(string name)
        {
            Name = name;
        }

        public static InventoryMovementReasons Create(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre de la razon del inventario debe de ser valida");

            return new InventoryMovementReasons(name.Trim());
        }

        public void Update(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre de la razon del inventario debe de ser valida");

            Name = name.Trim();
        }
    }
}
