using Factory.DAL.Models.OrderList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.OrderList
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.ItemName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(oi => oi.CustomerReference)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(oi => oi.Width)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.Height)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.SQM)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.TotalSQM)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.TotalLM)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
