using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Factory.DAL.Models.Support;

namespace Factory.DAL.Configurations.Support
{
    public class SupportResponseConfiguration : IEntityTypeConfiguration<SupportResponse>
    {
        public void Configure(EntityTypeBuilder<SupportResponse> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.ResponseText).IsRequired();
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);
            builder.HasOne(r => r.SupportTicket)
                   .WithMany(t => t.Responses)
                   .HasForeignKey(r => r.SupportTicketId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}