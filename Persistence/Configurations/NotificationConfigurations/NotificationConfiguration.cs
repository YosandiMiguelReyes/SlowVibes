using Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.NotificationConfigurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notifications>
    {
        public void Configure(EntityTypeBuilder<Notifications> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Type).HasMaxLength(20);
            builder.Property(n => n.Recipient).HasMaxLength(150);
            builder.Property(n => n.Status).HasConversion<string>().HasMaxLength(20);
        }
    }
}