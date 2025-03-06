using Factory.DAL.Models.OnBoarding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Onboarding
{
    public class PreboardingModuleConfiguration : IEntityTypeConfiguration<PreboardingModule>
    {
        public void Configure(EntityTypeBuilder<PreboardingModule> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DocumentsReceived)
                   .HasDefaultValue(false);

            builder.Property(p => p.ContractSigned)
                   .HasDefaultValue(false);

            builder.Property(p => p.BackgroundCheckCompleted)
                   .HasDefaultValue(false);

            builder.Property(p => p.WelcomeEmailSent)
                   .HasDefaultValue(false);

            builder.Property(p => p.Notes)
                   .HasMaxLength(500);

            builder.HasOne(p => p.OnboardingProcess)
                   .WithOne(o => o.Preboarding)
                   .HasForeignKey<PreboardingModule>(p => p.OnboardingProcessId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}