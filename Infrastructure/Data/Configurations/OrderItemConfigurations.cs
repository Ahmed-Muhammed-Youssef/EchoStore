using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderedProductInfo>
    {
        public void Configure(EntityTypeBuilder<OrderedProductInfo> builder)
        {
            // Properties
            builder.Property(opi => opi.Quantity)
                .IsRequired();

            // Relationships
            // productInfo
            builder.HasOne(opi => opi.ProductInfo)
                .WithMany()
                .HasForeignKey(opi => opi.ProductInfoId)
                .IsRequired();
        }
    }
}
