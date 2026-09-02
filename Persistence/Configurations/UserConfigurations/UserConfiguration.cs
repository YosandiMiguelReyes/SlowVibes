using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("UserId");

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(u => u.UserName)
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(u => u.Phone)
                   .HasMaxLength(20);

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.IsActive)
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired();
        }
    }
}