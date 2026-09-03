using Domain.Entities.Product;
using Domain.Entities.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ProductConfigurations
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.SKU).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(150);
            builder.Property(p => p.ImageUrl).HasMaxLength(500);
            builder.Property(p => p.SKU).HasMaxLength(50);
            builder.Property(p => p.PurchasePrice).HasPrecision(18, 2);
            builder.Property(p => p.SalePrice).HasPrecision(18, 2);

            builder.HasOne<Categories>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}