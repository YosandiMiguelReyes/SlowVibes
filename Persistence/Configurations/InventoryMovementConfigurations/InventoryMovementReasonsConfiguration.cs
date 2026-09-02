using Domain.Entities.InventoryMovement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.InventoryMovementConfigurations
{
    public class InventoryMovementReasonsConfiguration : IEntityTypeConfiguration<InventoryMovementReasons>
    {
        public void Configure(EntityTypeBuilder<InventoryMovementReasons> builder)
        {
            builder.HasKey(imr => imr.Id);
            builder.Property(imr => imr.Name).IsRequired().HasMaxLength(50);
        }
    }
}