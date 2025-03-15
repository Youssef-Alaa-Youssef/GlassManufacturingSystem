using Factory.DAL.Models.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(n => n.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(n => n.CreatedAt)
                .IsRequired();

            builder.Property(n => n.IsRead)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(n => n.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(n => n.IconClass)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(n => n.UserId)
                .IsRequired()
                .HasMaxLength(100);


            builder.HasIndex(n => n.UserId); 
            builder.HasIndex(n => n.IsRead); 
            builder.HasIndex(n => n.CreatedAt); 

             //builder.HasOne(n => n.User)
             //   .WithMany(u => u.Notifications)
             //   .HasForeignKey(n => n.UserId)
             //   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}