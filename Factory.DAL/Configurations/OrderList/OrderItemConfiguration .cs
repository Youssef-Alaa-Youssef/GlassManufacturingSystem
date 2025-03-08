using Factory.DAL.Models.OrderList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.ItemName)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(oi => oi.Description)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(oi => oi.Width)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(oi => oi.Height)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(oi => oi.StepWidth)
                   .HasPrecision(18, 2);

            builder.Property(oi => oi.StepHeight)
                   .HasPrecision(18, 2);
            builder.Property(o => o.Rank)
                 .HasPrecision(18, 2);

            builder.Property(oi => oi.SQM)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(oi => oi.TotalLM)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(oi => oi.IsDelivered)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(oi => oi.DeliveryDate)
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.Property(oi => oi.DeliveredBy)
                   .HasMaxLength(255)
                   .IsRequired(false);
        }
    }
}
