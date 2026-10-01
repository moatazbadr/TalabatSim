using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Domain.Entities.Order;

namespace Talabat.Repository.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
      
        builder.Property(oi=>oi.price).HasColumnType("decimal(18,2)");

        builder.OwnsOne(oi => oi.ItemOrdered, p => p.WithOwner());



    }
}
