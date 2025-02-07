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
                .HasMaxLength(100);

            builder.Property(o => o.CustomerReference)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.ProjectName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.JobNo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.Priority)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(o => o.SelectedMachines)
                .IsRequired();

            builder.Property(o => o.TotalSQM)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.TotalLM)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Signature)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
