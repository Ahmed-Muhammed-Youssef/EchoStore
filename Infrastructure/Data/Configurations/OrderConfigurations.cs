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

            // properties
            builder.Property(o => o.Status)
                .HasConversion<string>(s => s.ToString(), s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s))
                .IsRequired();
            builder.Property(o => o.BuyerEmail)
                .IsRequired();
            builder.Property(o => o.OrderDate)
                .IsRequired();
            builder.Property(o => o.Subtotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            
            builder.OwnsOne(O => O.ShipToAddress, a => { a.WithOwner(); });
            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.DeliveryMethod)
                .WithOne();
        }

    }
}
