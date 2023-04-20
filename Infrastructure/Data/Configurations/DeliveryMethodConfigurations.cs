using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(dm => dm.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(dm => dm.DeliveryTime).IsRequired();
            builder.Property(dm => dm.Desciption).IsRequired();
            builder.Property(dm => dm.ShortName).IsRequired();

            builder.HasOne<Order>()
                .WithOne(o => o.DeliveryMethod)
                .HasForeignKey<Order>(o => o.DeliveryMethodId)
                .IsRequired();
        }
    }
}
