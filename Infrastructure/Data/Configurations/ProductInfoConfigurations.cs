using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductInfoConfigurations : IEntityTypeConfiguration<ProductInfo>
    {
        public void Configure(EntityTypeBuilder<ProductInfo> builder)
        {
            // Properties
            builder.Property(pi => pi.Name)
                .HasMaxLength(60)
                .IsRequired();
            builder.Property(pi => pi.Description)
               .HasMaxLength(600)
               .IsRequired();
            builder.Property(pi => pi.Price)
               .IsRequired();
            builder.Property(pi => pi.AvailableAmount)
              .IsRequired();
            builder.Property(pi => pi.Rate)
              .IsRequired();
            builder.Property(pi => pi.PictureUrl)
              .IsRequired();

            // Relationsships
            // ProductType
            builder.HasOne(pi => pi.ProductType)
                .WithMany()
                .HasForeignKey(pi => pi.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            // ProductBrand
            builder.HasOne(pi => pi.ProductBrand)
                .WithMany()
                .HasForeignKey(pi => pi.ProductBrandId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }
    }
}
