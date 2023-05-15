using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Relationships
            // ProductInfo
            builder.HasOne(p => p.ProductInfo)
                .WithMany(pi => pi.Products)
                .HasForeignKey(p => p.ProductInfoId)
                .IsRequired();
        }
    }
}
