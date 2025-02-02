using Factory.DAL.Models.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Warehouses
{
    public class MainWarehouseConfiguration : IEntityTypeConfiguration<MainWarehouse>
    {
        public void Configure(EntityTypeBuilder<MainWarehouse> builder)
        {
            builder.ToTable("MainWarehouses");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.NameEn)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.NameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.AddressEn)
                .HasMaxLength(255);

            builder.Property(m => m.AddressAr)
                .HasMaxLength(255);

            builder.HasIndex(m => m.NameEn).IsUnique();
            builder.HasIndex(m => m.NameAr).IsUnique();

            // Seed Data for MainWarehouses
            builder.HasData(
                new MainWarehouse
                {
                    Id = 1,
                    NameEn = "Main Warehouse",
                    NameAr = "المستودع الرئيسي",
                    AddressEn = "Factory A, Floor 1",
                    AddressAr = "الطابق الأول، المصنع أ"
                }
            );
        }
    }
}