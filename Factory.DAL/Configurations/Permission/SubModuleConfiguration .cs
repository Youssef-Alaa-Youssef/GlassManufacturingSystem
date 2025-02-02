using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Factory.DAL.Models.Permission;

namespace Factory.DAL.Configurations.Permission
{
    public class SubModuleConfiguration : IEntityTypeConfiguration<SubModule>
    {
        public void Configure(EntityTypeBuilder<SubModule> builder)
        {
            builder.ToTable("SubModules");

            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sm => sm.Controller)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sm => sm.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sm => sm.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(sm => sm.Module)
                .WithMany(m => m.SubModules)
                .HasForeignKey(sm => sm.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new SubModule { Id = 1, Name = "Permission Management", Controller = "PermissionManagement", Action = "index", Title = "Mange Permssions", ModuleId = 1 },
                new SubModule { Id = 2, Name = "Assign Permission", Controller = "PermissionManagement", Action = "AssignPermissions", Title = "Assign Permssions", ModuleId = 1 },

                new SubModule { Id = 3, Name = "Modules Management", Controller = "Module", Action = "index", Title = "Modules Management", ModuleId = 1 },
                new SubModule { Id = 4, Name = "Sub Modules Management", Controller = "SubModule", Action = "Index", Title = "Sub Modules Management", ModuleId = 1 },

                new SubModule { Id = 5, Name = "User Management", Controller = "Auth", Action = "index", Title = "User Management", ModuleId = 2 },
                new SubModule { Id = 6, Name = "Role Management", Controller = "Role", Action = "index", Title = "Role Management", ModuleId = 3 }
            );
        }
    }
}