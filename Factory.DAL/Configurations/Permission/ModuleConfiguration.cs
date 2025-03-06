using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Factory.DAL.Models.Permission;

namespace Factory.DAL.Configurations.Permission
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Modules");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Url)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(m => m.SubModules)
                .WithOne(sm => sm.Module)
                .HasForeignKey(sm => sm.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                 new Module { Id = 1, Name = "Permission Management", Url = "/Users/Index", IconClass = "bi bi-shield-lock" },
                 new Module { Id = 2, Name = "User Management", Url = "/Auth/Index", IconClass = "bi bi-people" },
                 new Module { Id = 3, Name = "Role Management", Url = "/Role/Index", IconClass = "bi bi-person-badge" },
                 new Module { Id = 4, Name = "Warehouse Management", Url = "/Warehouse/Index", IconClass = "bi bi-box-seam" },
                 new Module { Id = 5, Name = "Orders", Url = "/Order/Index", IconClass = "bi bi-cart" },
                 new Module { Id = 6, Name = "Payroll", Url = "/Payroll/Index", IconClass = "bi bi-cash" },  
                 new Module { Id = 7, Name = "Settings", Url = "/Settings/Index", IconClass = "bi bi-gear" } ,
                 new Module { Id = 8, Name = "Financial", Url = "/Accountant/Index", IconClass = "bi bi-cash-stack" } 
             );
        }
    }
}