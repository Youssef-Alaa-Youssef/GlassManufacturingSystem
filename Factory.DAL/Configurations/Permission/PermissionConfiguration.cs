using Factory.DAL.Models.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.DAL.Configurations.Permission
{
    public class PermissionConfiguration : IEntityTypeConfiguration<PermissionTyepe>
    {
        public void Configure(EntityTypeBuilder<PermissionTyepe> builder)
        {
            builder.ToTable("PermissionTyepe");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new PermissionTyepe { Id = 1, Name = "Create" },
                new PermissionTyepe { Id = 2, Name = "Read" },
                new PermissionTyepe { Id = 3, Name = "Update" },
                new PermissionTyepe { Id = 4, Name = "Delete" }
            );
        }
    }
}