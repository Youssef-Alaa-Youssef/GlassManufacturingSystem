using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Factory.DAL.Models.OrderList;

namespace Factory.DAL.Configurations.OrderList
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.ItemName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Width)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(i => i.Height)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(i => i.Quantity)
                .IsRequired();

            builder.Property(i => i.SQM)
                .HasColumnType("decimal(18, 2)");

            builder.Property(i => i.TotalSQM)
                .HasColumnType("decimal(18, 2)");

            builder.Property(i => i.TotalLM)
                .HasColumnType("decimal(18, 2)");

            builder.Property(i => i.CustomerReference)
                .HasMaxLength(50);

            builder.HasOne(i => i.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
