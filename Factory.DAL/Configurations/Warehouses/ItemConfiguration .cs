using Factory.DAL.Models.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Warehouses
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Description)
                .HasMaxLength(500);

            builder.Property(i => i.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Color)
                .HasMaxLength(50);

            builder.Property(i => i.Thickness)
                .HasMaxLength(20);

            builder.Property(i => i.Dimensions)
                .HasMaxLength(50);

            builder.Property(i => i.Quantity)
                .IsRequired();

            builder.Property(i => i.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Ignore(i => i.TotalValue);

            builder.HasOne(i => i.Warehouse)
                .WithMany()
                .HasForeignKey(i => i.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.SubWarehouse)
                .WithMany()
                .HasForeignKey(i => i.SubWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(i => i.Manufacturer)
                .HasMaxLength(100);

            builder.Property(i => i.ManufactureDate)
                .IsRequired();

            builder.Property(i => i.ExpiryDate)
                .IsRequired();

            builder.Property(i => i.IsFragile)
                .IsRequired();

            builder.Property(i => i.Notes)
                .HasMaxLength(1000);

            builder.HasData(
                new Item
                {
                    Id = 1,
                    Name = "Glass Panel",
                    Description = "Transparent glass panel",
                    Type = "Panel",
                    Color = "Clear",
                    Thickness = "5mm",
                    Dimensions = "1200x800mm",
                    Quantity = 100,
                    UnitPrice = 50.00m,
                    WarehouseId = 1,
                    SubWarehouseId = 1, 
                    Manufacturer = "GlassCo",
                    ManufactureDate = new DateTime(2023, 1, 1),
                    ExpiryDate = new DateTime(2025, 1, 1),
                    IsFragile = true,
                    Notes = "Handle with care"
                },
                new Item
                {
                    Id = 2,
                    Name = "Tempered Glass",
                    Description = "Strong tempered glass",
                    Type = "Tempered",
                    Color = "Tinted",
                    Thickness = "10mm",
                    Dimensions = "1500x1000mm",
                    Quantity = 50,
                    UnitPrice = 100.00m,
                    WarehouseId = 1,
                    SubWarehouseId = 2, 
                    Manufacturer = "TemperedGlassCo",
                    ManufactureDate = new DateTime(2023, 2, 1),
                    ExpiryDate = new DateTime(2025, 2, 1),
                    IsFragile = true,
                    Notes = "Heat-resistant"
                }
            );
        }
    }
}