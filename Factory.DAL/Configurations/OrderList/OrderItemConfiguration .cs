using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Factory.DAL.Models.OrderList;

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
        }
    }
}
