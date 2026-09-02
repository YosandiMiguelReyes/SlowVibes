using Domain.Entities.Payment;
using Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.PaymentConfigurations
{
    public class PaymentsConfiguration : IEntityTypeConfiguration<Payments>
    {
        public void Configure(EntityTypeBuilder<Payments> builder)
        {
            builder.HasKey(p => p.Id);


            builder.HasOne<Orders>()
                .WithMany()
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.Property(p => p.PaymentMethod)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }
}