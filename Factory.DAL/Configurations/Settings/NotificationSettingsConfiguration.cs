using Factory.DAL.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Data.Configurations
{
    public class NotificationSettingsConfiguration : IEntityTypeConfiguration<NotificationSettings>
    {
        public void Configure(EntityTypeBuilder<NotificationSettings> builder)
        {
            builder.Property(n => n.EnableEmailNotifications)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(n => n.EnableSmsNotifications)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(n => n.EnablePushNotifications)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(n => n.EmailAddress)
                .HasMaxLength(150);

            builder.Property(n => n.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(n => n.Frequency)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(n => n.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(n => n.UpdatedDate)
                .IsRequired(false);

            builder.Property(n => n.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}