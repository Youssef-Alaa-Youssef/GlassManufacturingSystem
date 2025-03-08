using Factory.DAL.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Data.Configurations
{
    public class ContractSettingsConfiguration : IEntityTypeConfiguration<ContractSettings>
    {
        public void Configure(EntityTypeBuilder<ContractSettings> builder)
        {
            builder.Property(c => c.ContractStartDate)
                .IsRequired();

            builder.Property(c => c.ContractEndDate)
                .IsRequired();

            builder.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.UpdatedDate)
                .IsRequired(false);

            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}