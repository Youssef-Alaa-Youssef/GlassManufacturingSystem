using Factory.DAL.Models.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Finance
{
    public class FinancialRecordConfiguration : IEntityTypeConfiguration<FinancialRecord>
    {
        public void Configure(EntityTypeBuilder<FinancialRecord> builder)
        {
            builder.ToTable("FinancialRecords");

            builder.HasKey(fr => fr.Id);

            builder.Property(fr => fr.Description)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(fr => fr.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(fr => fr.TransactionType)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(fr => fr.Date)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(fr => fr.UserId)
                   .HasMaxLength(450)
                   .IsRequired();

            builder.HasIndex(fr => fr.Date)
                   .HasDatabaseName("IX_FinancialRecords_Date");
        }
    }
}
