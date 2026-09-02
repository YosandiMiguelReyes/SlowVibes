using Domain.Entities.InventoryMovement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.InventoryMovementConfigurations
{
    public class InventoryMovementTypesConfiguration : IEntityTypeConfiguration<InventoryMovementTypes>
    {
        public void Configure(EntityTypeBuilder<InventoryMovementTypes> builder)
        {
            builder.HasKey(imt => imt.Id);
            builder.Property(imt => imt.Name).IsRequired().HasMaxLength(20);
        }
    }
}