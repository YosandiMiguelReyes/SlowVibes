using Domain.Entities.InventoryMovement;
using Domain.Entities.Product;
using Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.InventoryMovementConfigurations
{
    public class InventoryMovementsConfiguration : IEntityTypeConfiguration<InventoryMovements>
    {

        public void Configure(EntityTypeBuilder<InventoryMovements> builder)
        {
            builder.HasKey(im => im.Id);

            builder.HasOne<Products>()
                .WithMany()
                .HasForeignKey(im => im.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Orders>()
                .WithMany()
                .HasForeignKey(im => im.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<InventoryMovementTypes>()
                .WithMany()
                .HasForeignKey(im => im.MovementTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<InventoryMovementReasons>()
                .WithMany()
                .HasForeignKey(im => im.ReasonId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}