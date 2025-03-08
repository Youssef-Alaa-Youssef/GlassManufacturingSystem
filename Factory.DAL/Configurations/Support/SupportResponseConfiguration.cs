using Factory.DAL.Models.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Support
{
    public class SupportResponseConfiguration : IEntityTypeConfiguration<SupportResponse>
    {
        public void Configure(EntityTypeBuilder<SupportResponse> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.ResponseText)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(r => r.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(r => r.UpdatedAt)
                   .IsRequired(false);

            builder.Property(r => r.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasOne(r => r.SupportTicket)
                   .WithMany(t => t.Responses)
                   .HasForeignKey(r => r.SupportTicketId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            builder.HasOne(r => r.RespondedByUser)
                   .WithMany() // Assuming ApplicationUser does not have a navigation property for SupportResponse
                   .HasForeignKey(r => r.RespondedByUserId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}