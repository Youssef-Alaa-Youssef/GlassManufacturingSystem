using Factory.DAL.Models.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Warehouses
{
    public class SubWarehouseConfiguration : IEntityTypeConfiguration<SubWarehouse>
    {
        public void Configure(EntityTypeBuilder<SubWarehouse> builder)
        {
            builder.ToTable("SubWarehouses");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.NameEn)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.NameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.AddressEn)
                .HasMaxLength(255);

            builder.Property(s => s.AddressAr)
                .HasMaxLength(255);

            builder.HasOne(s => s.MainWarehouse)
                .WithMany(m => m.SubWarehouses)
                .HasForeignKey(s => s.MainWarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => s.NameEn).IsUnique();
            builder.HasIndex(s => s.NameAr).IsUnique();

            // Seed Data for SubWarehouses
            builder.HasData(
                new SubWarehouse
                {
                    Id = 1,
                    NameEn = "Sub-Warehouse A",
                    NameAr = "المستودع الفرعي أ",
                    AddressEn = "Main Warehouse, Section 1",
                    AddressAr = "القسم الأول، المستودع الرئيسي",
                    MainWarehouseId = 1
                },
                new SubWarehouse
                {
                    Id = 2,
                    NameEn = "Sub-Warehouse B",
                    NameAr = "المستودع الفرعي ب",
                    AddressEn = "Main Warehouse, Section 2",
                    AddressAr = "القسم الثاني، المستودع الرئيسي",
                    MainWarehouseId = 1
                }
            );
        }
    }
}