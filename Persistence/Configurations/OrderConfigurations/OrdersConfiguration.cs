using Domain.Entities.Order;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.OrderConfigurations
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.ShippingAddress).HasMaxLength(255);
            builder.Property(o => o.TotalAmount).HasPrecision(18, 2);
            

            builder.HasOne<Users>()
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(o => o.OrderStatus)
                .HasConversion<string>()
                .HasMaxLength(20);  

            builder.Property(o => o.OrderSource)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(o => o.DeliveryType)
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }
}