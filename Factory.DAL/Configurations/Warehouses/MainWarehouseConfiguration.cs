using Factory.DAL.Enums.Stores;
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

            // Primary Key
            builder.HasKey(m => m.Id);

            // Properties Configuration
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

            builder.Property(m => m.Capacity)
                .IsRequired();

            builder.Property(m => m.CurrentStock)
                .IsRequired();

            builder.Property(m => m.Type)
                .IsRequired();

            builder.Property(m => m.Status)
                .IsRequired();

            builder.Property(m => m.Manager)
                .HasMaxLength(100);

            builder.Property(m => m.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(m => m.Email)
                .HasMaxLength(100);

            // Indexes
            builder.HasIndex(m => m.NameEn).IsUnique();
            builder.HasIndex(m => m.NameAr).IsUnique();

        }
    }
}