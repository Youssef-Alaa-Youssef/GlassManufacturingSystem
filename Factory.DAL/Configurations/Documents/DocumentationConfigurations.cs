using Factory.DAL.Models.Documentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Documents
{
    public class DocumentationConfigurations : IEntityTypeConfiguration<Documentation>
    {
        public void Configure(EntityTypeBuilder<Documentation> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(d => d.Content)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(d => d.VideoUrl)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(d => d.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(d => d.UserId)
                .IsRequired()
                .HasMaxLength(450);
        }
    }
}
