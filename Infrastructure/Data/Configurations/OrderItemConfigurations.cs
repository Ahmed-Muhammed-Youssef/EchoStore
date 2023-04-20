using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(oi => oi.ItemOrdered, i => { i.WithOwner(); });
            builder.Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
