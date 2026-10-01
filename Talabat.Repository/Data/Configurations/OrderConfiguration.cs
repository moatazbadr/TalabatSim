using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Domain.Entities.Order;

namespace Talabat.Repository.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(O => O.Status)
            .HasConversion(Ostatus => Ostatus.ToString(), Ostatus => Enum.Parse<OrderStatus>(Ostatus));

        builder.Property(O=>O.Subtotal)
            .HasColumnType("decimal(18,2)");

        builder.OwnsOne(O=>O.ShippingAddress, builder =>
        {
            builder.WithOwner();
        });

        builder.HasOne(dm=>dm.DeliveryMethod).WithMany()
            .OnDelete(DeleteBehavior.NoAction);




    }
}
