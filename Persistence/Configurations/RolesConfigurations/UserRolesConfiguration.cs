using Domain.Entities.Roles;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.RolesConfigurations
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.HasKey(ur => ur.Id);
            
            builder.Property(ur => ur.Role)
                    .HasConversion<string>()
                    .HasMaxLength(20);

            builder.HasOne<Users>()
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}