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
                // Core Management Modules
                new Module { Id = 1, Name = "Permission Management", Url = "/Users/Index", IconClass = "bi bi-shield-lock" },
                new Module { Id = 2, Name = "User Management", Url = "/Auth/Index", IconClass = "bi bi-people" },
                new Module { Id = 3, Name = "Role Management", Url = "/Role/Index", IconClass = "bi bi-person-badge" },
                new Module { Id = 4, Name = "Warehouse Management", Url = "/Warehouse/Index", IconClass = "bi bi-box-seam" },
                new Module { Id = 5, Name = "Orders", Url = "/Order/Index", IconClass = "bi bi-cart" },
                new Module { Id = 6, Name = "Payroll", Url = "/Payroll/Index", IconClass = "bi bi-cash" },
                new Module { Id = 7, Name = "Settings", Url = "/Settings/Index", IconClass = "bi bi-gear" },
                new Module { Id = 8, Name = "Financial", Url = "/Accountant/Index", IconClass = "bi bi-cash-stack" },

                // Employee Lifecycle
                new Module { Id = 9, Name = "Onboarding", Url = "/Onboarding/Index", IconClass = "bi bi-person-plus" },
                new Module { Id = 10, Name = "Offboarding", Url = "/Offboarding/Index", IconClass = "bi bi-person-dash" },

                // Human Resources & Performance
                new Module { Id = 11, Name = "HR Management", Url = "/HR/Index", IconClass = "bi bi-briefcase" },
                new Module { Id = 12, Name = "Performance Management", Url = "/Performance/Index", IconClass = "bi bi-graph-up" },
                new Module { Id = 13, Name = "Employee Self-Service", Url = "/ESS/Index", IconClass = "bi bi-person-circle" },

                // IT & Security
                new Module { Id = 14, Name = "IT Service Desk", Url = "/ITService/Index", IconClass = "bi bi-laptop" },
                new Module { Id = 15, Name = "Access Control", Url = "/AccessControl/Index", IconClass = "bi bi-key" },

                // Project & Workflow
                new Module { Id = 16, Name = "Project Management", Url = "/Project/Index", IconClass = "bi bi-kanban" },
                new Module { Id = 17, Name = "Workflow Automation", Url = "/Workflow/Index", IconClass = "bi bi-lightning" },

                // Customer Relations
                new Module { Id = 18, Name = "Customer Support", Url = "/Support/Index", IconClass = "bi bi-headset" },
                new Module { Id = 19, Name = "CRM", Url = "/CRM/Index", IconClass = "bi bi-person-lines-fill" },

                // Analytics & Reporting
                new Module { Id = 20, Name = "Analytics & Reporting", Url = "/Reports/Index", IconClass = "bi bi-bar-chart-line" }
            );
        }
    }
}