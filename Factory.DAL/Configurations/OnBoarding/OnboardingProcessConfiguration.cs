using Factory.DAL.Models.OnBoarding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Onboarding
{
    public class OnboardingProcessConfiguration : IEntityTypeConfiguration<OnboardingProcess>
    {
        public void Configure(EntityTypeBuilder<OnboardingProcess> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.EmployeeName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(o => o.EmployeeId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(o => o.StartDate)
                   .IsRequired();

            builder.Property(o => o.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.HasOne(o => o.Preboarding)
                   .WithOne(p => p.OnboardingProcess)
                   .HasForeignKey<PreboardingModule>(p => p.OnboardingProcessId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.ITSetup)
                   .WithOne(i => i.OnboardingProcess)
                   .HasForeignKey<ITSetupModule>(i => i.OnboardingProcessId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Training)
                   .WithOne(t => t.OnboardingProcess)
                   .HasForeignKey<TrainingModule>(t => t.OnboardingProcessId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Orientation)
                   .WithOne(o => o.OnboardingProcess)
                   .HasForeignKey<OrientationModule>(o => o.OnboardingProcessId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}