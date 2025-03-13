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

            builder.Property(i => i.CodeNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.NameEn)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.NameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.DescriptionEn)
                .HasMaxLength(250);

            builder.Property(i => i.DescriptionAr)
                .HasMaxLength(250);

            builder.Property(i => i.UnitPrice)
                .HasColumnType("decimal(18, 2)");

            builder.Property(i => i.MinimumStock)
                .HasDefaultValue(10);

            builder.Property(i => i.CurrentStock)
                .HasDefaultValue(0);

            builder.Property(i => i.UnitOfMeasure)
                .HasDefaultValue("Piece");

            builder.Property(i => i.IsActive)
                .HasDefaultValue(true);

            builder.Property(i => i.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(i => i.UpdatedDate)
                .IsRequired(false);

            builder.Property(i => i.Thickness)
                .HasDefaultValue(4.0);

            builder.Property(i => i.Width)
                .HasDefaultValue(0.0);

            builder.Property(i => i.Height)
                .HasDefaultValue(0.0);

            builder.Property(i => i.Color)
                .HasDefaultValue("Clear");

            builder.Property(i => i.Quality)
                .HasDefaultValue("Standard");

            builder.Property(i => i.IsToughened)
                .HasDefaultValue(false);

            builder.Property(i => i.IsLaminated)
                .HasDefaultValue(false);

            // Relationships
            builder.HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.MainWarehouse)
                .WithMany()
                .HasForeignKey(i => i.MainWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.SubWarehouse)
                .WithMany()
                .HasForeignKey(i => i.SubWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}