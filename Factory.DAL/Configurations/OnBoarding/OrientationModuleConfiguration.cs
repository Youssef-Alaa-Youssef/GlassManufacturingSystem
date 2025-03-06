using Factory.DAL.Models.OnBoarding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Onboarding
{
    public class OrientationModuleConfiguration : IEntityTypeConfiguration<OrientationModule>
    {
        public void Configure(EntityTypeBuilder<OrientationModule> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.CompanyOrientationCompleted)
                   .HasDefaultValue(false);

            builder.Property(o => o.DepartmentOrientationCompleted)
                   .HasDefaultValue(false);

            builder.Property(o => o.MentorAssigned)
                   .HasDefaultValue(false);

            builder.Property(o => o.FirstWeekCheckInCompleted)
                   .HasDefaultValue(false);

            builder.Property(o => o.Notes)
                   .HasMaxLength(500);

            builder.HasOne(o => o.OnboardingProcess)
                   .WithOne(o => o.Orientation)
                   .HasForeignKey<OrientationModule>(o => o.OnboardingProcessId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}