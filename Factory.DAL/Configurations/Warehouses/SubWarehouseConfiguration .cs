using Factory.DAL.Enums.Stores;
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

            // Primary Key
            builder.HasKey(s => s.Id);

            // Properties Configuration
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

            builder.Property(s => s.Capacity)
                .IsRequired();

            builder.Property(s => s.CurrentStock)
                .IsRequired();

            builder.Property(s => s.Type)
                .IsRequired();

            builder.Property(s => s.Status)
                .IsRequired();

            builder.Property(s => s.Manager)
                .HasMaxLength(100);

            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(s => s.Email)
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(s => s.MainWarehouse)
                .WithMany(m => m.SubWarehouses)
                .HasForeignKey(s => s.MainWarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(s => s.NameEn).IsUnique();
            builder.HasIndex(s => s.NameAr).IsUnique();
        }
    }
}