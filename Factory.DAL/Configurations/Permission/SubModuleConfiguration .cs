using Factory.DAL.Models.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Permission
{
    public class SubModuleConfiguration : IEntityTypeConfiguration<SubModule>
    {
        public void Configure(EntityTypeBuilder<SubModule> builder)
        {
            builder.ToTable("SubModules");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.IconClass)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ModuleId)
                .IsRequired();

        }
    }
}