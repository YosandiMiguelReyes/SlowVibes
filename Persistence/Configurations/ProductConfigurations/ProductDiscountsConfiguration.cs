using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ProductConfigurations
{
    public class ProductDiscountsConfiguration : IEntityTypeConfiguration<ProductDiscounts>
    {
        public void Configure(EntityTypeBuilder<ProductDiscounts> builder)
        {
            builder.HasKey(pd => pd.Id);

            builder.HasOne<Products>()
                .WithMany()
                .HasForeignKey(pd => pd.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}