using Factory.DAL.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Settings
{
    public class ExportImportSettingsConfigurations : IEntityTypeConfiguration<ExportImportSettings>
    {
        public void Configure(EntityTypeBuilder<ExportImportSettings> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.EnableExport)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e => e.EnableImport)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e => e.SupportedExportFormats)
                .IsRequired()
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to List<string>
                );

            builder.Property(e => e.SupportedImportFormats)
                .IsRequired()
                .HasConversion(
                    v => string.Join(',', v), 
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to List<string>
                );

            builder.Property(e => e.MaxExportRows)
                .IsRequired()
                .HasDefaultValue(10000);

            builder.Property(e => e.MaxImportFileSize)
                .IsRequired()
                .HasDefaultValue(10 * 1024 * 1024); // 10MB

            builder.Property(e => e.IncludeHeaders)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e => e.DateFormat)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("yyyy-MM-dd");

            builder.Property(e => e.DefaultExportFormat)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("XLSX");

            builder.Property(e => e.CsvDelimiter)
                .IsRequired()
                .HasDefaultValue(',');

            builder.Property(e => e.AllowNullValues)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e => e.ValidateImportData)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
                .IsRequired(false);
        }
    }
}