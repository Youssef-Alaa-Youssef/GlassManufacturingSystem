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
                // Permission Management
                new SubModule { Id = 1, Name = "Permission Management", Controller = "PermissionManagement", Action = "index", Title = "Manage Permissions", ModuleId = 1 },
                new SubModule { Id = 2, Name = "Assign Permission", Controller = "PermissionManagement", Action = "AssignPermissions", Title = "Assign Permissions", ModuleId = 1 },

                // Module Management
                new SubModule { Id = 3, Name = "Modules Management", Controller = "Module", Action = "index", Title = "Modules Management", ModuleId = 1 },
                new SubModule { Id = 4, Name = "Sub Modules Management", Controller = "SubModule", Action = "Index", Title = "Sub Modules Management", ModuleId = 1 },

                // User & Role Management
                new SubModule { Id = 5, Name = "User Management", Controller = "Auth", Action = "index", Title = "User Management", ModuleId = 2 },
                new SubModule { Id = 6, Name = "Role Management", Controller = "Role", Action = "index", Title = "Role Management", ModuleId = 3 },

                // Warehouse Management
                new SubModule { Id = 7, Name = "Warehouse Management", Controller = "Warehouse", Action = "index", Title = "Warehouse Management", ModuleId = 4 },
                new SubModule { Id = 8, Name = "Item Management", Controller = "Item", Action = "index", Title = "Item Management", ModuleId = 4 },

                // Order Management
                new SubModule { Id = 9, Name = "Create Order", Controller = "Order", Action = "Create", Title = "Order Creation", ModuleId = 5 },
                new SubModule { Id = 10, Name = "View Orders", Controller = "Order", Action = "index", Title = "Order Management", ModuleId = 5 },

                // Settings 
                new SubModule { Id = 11, Name = "General Settings", Controller = "Settings", Action = "General", Title = "General Settings", ModuleId = 7 },
                new SubModule { Id = 12, Name = "Security Settings", Controller = "Settings", Action = "Security", Title = "Security Settings", ModuleId = 7 },

                //  Payroll
                new SubModule { Id = 13, Name = "Payroll Dashboard", Controller = "Payroll", Action = "Index", Title = "Payroll Dashboard", ModuleId = 6 },
                new SubModule { Id = 14, Name = "Employee Salaries", Controller = "Payroll", Action = "EmployeeSalaries", Title = "Employee Salaries", ModuleId = 6 },
                new SubModule { Id = 15, Name = "Salary Processing", Controller = "Payroll", Action = "ProcessSalaries", Title = "Salary Processing", ModuleId = 6 },
                new SubModule { Id = 16, Name = "Payroll Reports", Controller = "Payroll", Action = "Reports", Title = "Payroll Reports", ModuleId = 6 },
                new SubModule { Id = 17, Name = "Bonuses Management", Controller = "Payroll", Action = "Bonuses", Title = "Bonuses Management", ModuleId = 6 },
                new SubModule { Id = 18, Name = "Deductions", Controller = "Payroll", Action = "Deductions", Title = "Salary Deductions", ModuleId = 6 },
                new SubModule { Id = 19, Name = "Tax Calculations", Controller = "Payroll", Action = "Tax", Title = "Tax Calculations", ModuleId = 6 },
                new SubModule { Id = 20, Name = "Payslip Generation", Controller = "Payroll", Action = "GeneratePayslip", Title = "Payslip Generation", ModuleId = 6 },
                new SubModule { Id = 21, Name = "Overtime Payments", Controller = "Payroll", Action = "Overtime", Title = "Overtime Payments", ModuleId = 6 },
                new SubModule { Id = 22, Name = "Payroll History", Controller = "Payroll", Action = "History", Title = "Payroll History", ModuleId = 6 },
                new SubModule
                {
                    Id = 23,
                    Name = "Financial Orders",  
                    Controller = "Accountant",
                    Action = "Index",
                    Title = "Financial History", 
                    ModuleId = 8
                }
                );

        }
    }
}