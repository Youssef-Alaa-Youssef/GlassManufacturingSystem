using Factory.DAL.Models.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Support
{
    public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
    {
        public void Configure(EntityTypeBuilder<SupportTicket> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Status).HasDefaultValue("Open");
            builder.Property(t => t.Priority).HasDefaultValue("Medium");
            builder.Property(t => t.Type).HasDefaultValue("General");
            builder.HasMany(t => t.Responses)
                   .WithOne(r => r.SupportTicket)
                   .HasForeignKey(r => r.SupportTicketId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}