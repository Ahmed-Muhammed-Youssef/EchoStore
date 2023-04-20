using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Properties
            builder.Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.OwnsOne(oi => oi.ItemOrdered, i => { i.WithOwner(); });
        }
    }
}
