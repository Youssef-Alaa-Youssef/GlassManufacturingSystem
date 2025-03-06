using Factory.DAL.Models.OnBoarding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Onboarding
{
    public class TrainingModuleConfiguration : IEntityTypeConfiguration<TrainingModule>
    {
        public void Configure(EntityTypeBuilder<TrainingModule> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.ComplianceTraining)
                   .HasDefaultValue(false);

            builder.Property(t => t.SkillsTraining)
                   .HasDefaultValue(false);

            builder.Property(t => t.SystemsTraining)
                   .HasDefaultValue(false);

            builder.Property(t => t.SecurityTraining)
                   .HasDefaultValue(false);

            builder.Property(t => t.Notes)
                   .HasMaxLength(500);

            builder.HasOne(t => t.OnboardingProcess)
                   .WithOne(o => o.Training)
                   .HasForeignKey<TrainingModule>(t => t.OnboardingProcessId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}