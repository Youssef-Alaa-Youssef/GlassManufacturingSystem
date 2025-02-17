using Factory.DAL.Models.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(i => i.Description)
                .HasMaxLength(500);

            builder.Property(i => i.Type)
                .HasMaxLength(100);

            builder.Property(i => i.Color)
                .HasMaxLength(50);

            builder.Property(i => i.Thickness)
                .HasMaxLength(50);

            builder.Property(i => i.Width)
                .HasPrecision(10, 2);

            builder.Property(i => i.Height)
                .HasPrecision(10, 2);

            builder.Property(i => i.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(i => i.Quantity)
                .IsRequired();

            builder.Ignore(i => i.TotalValue); // Ignoring computed property

            builder.Property(i => i.Manufacturer)
                .HasMaxLength(255);

            builder.Property(i => i.ManufactureDate)
                .IsRequired();

            builder.Property(i => i.ExpiryDate)
                .IsRequired();

            builder.Property(i => i.IsFragile)
                .HasDefaultValue(false);

            builder.Property(i => i.Notes)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(i => i.Warehouse)
                .WithMany() // Assuming one-to-many
                .HasForeignKey(i => i.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.SubWarehouse)
                .WithMany() // Assuming one-to-many
                .HasForeignKey(i => i.SubWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
