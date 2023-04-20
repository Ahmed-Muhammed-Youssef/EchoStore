using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShipToAddress, a => { a.WithOwner(); });
            builder.Property(o => o.Status)
                .HasConversion<string>(s => s.ToString(), s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));
            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
