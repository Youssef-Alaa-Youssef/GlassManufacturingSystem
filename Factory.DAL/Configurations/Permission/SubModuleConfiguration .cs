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
                new SubModule { Id = 23,Name = "Financial Orders",  Controller = "Accountant",Action = "Index",Title = "Financial History", ModuleId = 8},
                new SubModule { Id = 24, Name = "Pre-Onboarding", Controller = "Onboarding", Action = "PreOnboarding", Title = "Pre-Onboarding Process", ModuleId = 9 },
                new SubModule { Id = 25, Name = "IT Setup", Controller = "Onboarding", Action = "ITSetup", Title = "IT System & Equipment Setup", ModuleId = 9 },
                new SubModule { Id = 26, Name = "Training & Orientation", Controller = "Onboarding", Action = "Training", Title = "Employee Training and Orientation", ModuleId = 9 },

                // Offboarding
                new SubModule { Id = 27, Name = "Exit Clearance", Controller = "Offboarding", Action = "Clearance", Title = "Employee Exit Clearance", ModuleId = 10 },
                new SubModule { Id = 28, Name = "Access Revocation", Controller = "Offboarding", Action = "RevokeAccess", Title = "Revoke IT & System Access", ModuleId = 10 },
                new SubModule { Id = 29, Name = "Final Payroll & Documents", Controller = "Offboarding", Action = "FinalPayroll", Title = "Final Payroll & Document Handling", ModuleId = 10 },

                // HR Management
                new SubModule { Id = 30, Name = "Employee Records", Controller = "HR", Action = "Records", Title = "Manage Employee Records", ModuleId = 11 },
                new SubModule { Id = 31, Name = "Leave Management", Controller = "HR", Action = "Leave", Title = "Manage Leaves & Absences", ModuleId = 11 },
                new SubModule { Id = 32, Name = "Payroll Processing", Controller = "HR", Action = "Payroll", Title = "Automate Payroll Processing", ModuleId = 11 },

                // Performance Management
                new SubModule { Id = 33, Name = "Performance Reviews", Controller = "Performance", Action = "Reviews", Title = "Employee Performance Reviews", ModuleId = 12 },
                new SubModule { Id = 34, Name = "KPI Tracking", Controller = "Performance", Action = "KPIs", Title = "Track KPIs & Goals", ModuleId = 12 },
                new SubModule { Id = 35, Name = "Feedback & Recognition", Controller = "Performance", Action = "Feedback", Title = "360 Feedback & Recognition", ModuleId = 12 },

                // IT Service Desk
                new SubModule { Id = 36, Name = "Ticket Management", Controller = "ITService", Action = "Tickets", Title = "Manage IT Support Tickets", ModuleId = 14 },
                new SubModule { Id = 37, Name = "System Monitoring", Controller = "ITService", Action = "Monitoring", Title = "Monitor IT Infrastructure", ModuleId = 14 },
                new SubModule { Id = 38, Name = "Hardware Inventory", Controller = "ITService", Action = "Inventory", Title = "Manage IT Assets", ModuleId = 14 },

                // Customer Support
                new SubModule { Id = 39, Name = "Support Tickets", Controller = "Support", Action = "Tickets", Title = "Manage Customer Tickets", ModuleId = 18 },
                new SubModule { Id = 40, Name = "Live Chat", Controller = "Support", Action = "Chat", Title = "Provide Live Chat Support", ModuleId = 18 },
                new SubModule { Id = 41, Name = "FAQ & Help Center", Controller = "Support", Action = "FAQ", Title = "Manage Help Center Articles", ModuleId = 18 },

                // Analytics & Reporting
                new SubModule { Id = 42, Name = "Financial Reports", Controller = "Reports", Action = "Finance", Title = "View Financial Reports", ModuleId = 20 },
                new SubModule { Id = 43, Name = "Employee Insights", Controller = "Reports", Action = "Employees", Title = "Analyze Employee Performance", ModuleId = 20 },
                new SubModule { Id = 44, Name = "Sales & Revenue", Controller = "Reports", Action = "Sales", Title = "Track Sales & Revenue", ModuleId = 20 },
                new SubModule { Id = 45, Name = "Support Dashboard", Controller = "Support", Action = "Index", Title = "View Support Overview", ModuleId = 18 }
                );

        }
    }
}