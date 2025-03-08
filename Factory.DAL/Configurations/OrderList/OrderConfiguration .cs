using Factory.DAL.Models.OrderList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.OrderList
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(o => o.CustomerReference)
                   .HasMaxLength(255);

            builder.Property(o => o.ProjectName)
                   .HasMaxLength(255);

            builder.Property(o => o.Priority)
                   .HasMaxLength(50);

            builder.Property(o => o.Status)
                   .HasMaxLength(50);

            builder.Property(o => o.Signature)
                       .HasMaxLength(255);

            builder.Property(o => o.TotalSQM)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(o => o.TotalLM)
                   .HasPrecision(18, 2)
                   .IsRequired();
            builder.Property(o => o.Rank)
              .HasPrecision(18, 2);

            builder.Property(o => o.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasMany(o => o.Items)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
