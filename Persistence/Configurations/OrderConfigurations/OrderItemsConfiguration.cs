using Domain.Entities.Order;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.OrderConfigurations
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.PurchasePrice)
                .HasPrecision(18, 2);

            builder.Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

            builder.Property(oi => oi.DiscountApplied)
                .HasPrecision(5, 2);

            builder.Property(oi => oi.Profit)
                .HasPrecision(18, 2);
                

            builder.HasOne<Orders>()
                .WithMany(oi => oi.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Products>()
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}