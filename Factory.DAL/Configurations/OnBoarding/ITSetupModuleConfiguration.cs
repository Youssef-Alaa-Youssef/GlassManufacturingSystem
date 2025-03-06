using Factory.DAL.Models.OnBoarding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Onboarding
{
    public class ITSetupModuleConfiguration : IEntityTypeConfiguration<ITSetupModule>
    {
        public void Configure(EntityTypeBuilder<ITSetupModule> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.EmailSetup)
                   .HasDefaultValue(false);

            builder.Property(i => i.HardwareProvisioned)
                   .HasDefaultValue(false);

            builder.Property(i => i.SoftwareInstalled)
                   .HasDefaultValue(false);

            builder.Property(i => i.AccessGranted)
                   .HasDefaultValue(false);

            builder.Property(i => i.Notes)
                   .HasMaxLength(500);

            builder.HasOne(i => i.OnboardingProcess)
                   .WithOne(o => o.ITSetup)
                   .HasForeignKey<ITSetupModule>(i => i.OnboardingProcessId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}