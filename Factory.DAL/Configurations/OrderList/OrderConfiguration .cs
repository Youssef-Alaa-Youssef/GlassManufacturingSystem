using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Factory.DAL.Models.OrderList;

namespace Factory.DAL.Configurations.OrderList
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.CustomerReference)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.ProjectName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Date)
                .IsRequired();

            builder.Property(o => o.JobNo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(o => o.Priority)
                .HasMaxLength(50);

            builder.Property(o => o.FinishDate)
                .IsRequired();

            builder.Property(o => o.Signature)
                .HasMaxLength(250);

            builder.Property(o => o.IsAccepted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
