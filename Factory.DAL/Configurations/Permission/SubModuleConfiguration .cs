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

            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sm => sm.Controller)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sm => sm.Action)
                .HasMaxLength(100);
            builder.Property(sm => sm.IconClass)
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
    new SubModule { Id = 1, Name = "Permission Management", IconClass = "bi-shield-lock", Controller = "PermissionManagement", Action = "index", Title = "Manage Permissions", ModuleId = 1 },
    new SubModule { Id = 2, Name = "Assign Permission", IconClass = "bi-person-check", Controller = "PermissionManagement", Action = "AssignPermissions", Title = "Assign Permissions", ModuleId = 1 },

    // Module Management
    new SubModule { Id = 3, Name = "Modules Management", IconClass = "bi-puzzle", Controller = "Module", Action = "index", Title = "Modules Management", ModuleId = 1 },
    new SubModule { Id = 4, Name = "Sub Modules Management", IconClass = "bi-puzzle-fill", Controller = "SubModule", Action = "Index", Title = "Sub Modules Management", ModuleId = 1 },

    // User & Role Management
    new SubModule { Id = 5, Name = "User Management", IconClass = "bi-people", Controller = "Auth", Action = "index", Title = "User Management", ModuleId = 2 },
    new SubModule { Id = 6, Name = "Role Management", IconClass = "bi-person-badge", Controller = "Role", Action = "index", Title = "Role Management", ModuleId = 3 },

    // Warehouse Management
    new SubModule { Id = 7, Name = "Warehouse Management", IconClass = "bi-house-door", Controller = "Warehouse", Action = "index", Title = "Warehouse Management", ModuleId = 4 },
    new SubModule { Id = 8, Name = "Item Management", IconClass = "bi-box-seam", Controller = "Item", Action = "index", Title = "Item Management", ModuleId = 4 },

    // Order Management
    new SubModule { Id = 9, Name = "Create Order", IconClass = "bi-cart-plus", Controller = "Order", Action = "Create", Title = "Order Creation", ModuleId = 5 },
    new SubModule { Id = 10, Name = "View Orders", IconClass = "bi-cart-check", Controller = "Order", Action = "index", Title = "Order Management", ModuleId = 5 },

    // Settings 
    new SubModule { Id = 11, Name = "General Settings", IconClass = "bi-gear", Controller = "Settings", Action = "General", Title = "General Settings", ModuleId = 7 },
    new SubModule { Id = 12, Name = "Security Settings", IconClass = "bi-shield", Controller = "Settings", Action = "Security", Title = "Security Settings", ModuleId = 7 },

    // Payroll
    new SubModule { Id = 13, Name = "Payroll Dashboard", IconClass = "bi-cash-stack", Controller = "Payroll", Action = "Index", Title = "Payroll Dashboard", ModuleId = 6 },
    new SubModule { Id = 14, Name = "Employee Salaries", IconClass = "bi-wallet", Controller = "Payroll", Action = "EmployeeSalaries", Title = "Employee Salaries", ModuleId = 6 },
    new SubModule { Id = 15, Name = "Salary Processing", IconClass = "bi-calculator", Controller = "Payroll", Action = "ProcessSalaries", Title = "Salary Processing", ModuleId = 6 },
    new SubModule { Id = 16, Name = "Payroll Reports", IconClass = "bi-file-earmark-bar-graph", Controller = "Payroll", Action = "Reports", Title = "Payroll Reports", ModuleId = 6 },
    new SubModule { Id = 17, Name = "Bonuses Management", IconClass = "bi-gift", Controller = "Payroll", Action = "Bonuses", Title = "Bonuses Management", ModuleId = 6 },
    new SubModule { Id = 18, Name = "Deductions", IconClass = "bi-dash-circle", Controller = "Payroll", Action = "Deductions", Title = "Salary Deductions", ModuleId = 6 },
    new SubModule { Id = 19, Name = "Tax Calculations", IconClass = "bi-percent", Controller = "Payroll", Action = "Tax", Title = "Tax Calculations", ModuleId = 6 },
    new SubModule { Id = 20, Name = "Payslip Generation", IconClass = "bi-receipt", Controller = "Payroll", Action = "GeneratePayslip", Title = "Payslip Generation", ModuleId = 6 },
    new SubModule { Id = 21, Name = "Overtime Payments", IconClass = "bi-clock-history", Controller = "Payroll", Action = "Overtime", Title = "Overtime Payments", ModuleId = 6 },
    new SubModule { Id = 22, Name = "Payroll History", IconClass = "bi-archive", Controller = "Payroll", Action = "History", Title = "Payroll History", ModuleId = 6 },

    // Financial Orders
    new SubModule { Id = 23, Name = "Financial Orders", IconClass = "bi-currency-dollar", Controller = "Accountant", Action = "Index", Title = "Financial History", ModuleId = 8 },

    // Onboarding
    new SubModule { Id = 24, Name = "Pre-Onboarding", IconClass = "bi-person-plus", Controller = "Onboarding", Action = "PreOnboarding", Title = "Pre-Onboarding Process", ModuleId = 9 },
    new SubModule { Id = 25, Name = "IT Setup", IconClass = "bi-laptop", Controller = "Onboarding", Action = "ITSetup", Title = "IT System & Equipment Setup", ModuleId = 9 },
    new SubModule { Id = 26, Name = "Training & Orientation", IconClass = "bi-book", Controller = "Onboarding", Action = "Training", Title = "Employee Training and Orientation", ModuleId = 9 },

    // Offboarding
    new SubModule { Id = 27, Name = "Exit Clearance", IconClass = "bi-door-open", Controller = "Offboarding", Action = "Clearance", Title = "Employee Exit Clearance", ModuleId = 10 },
    new SubModule { Id = 28, Name = "Access Revocation", IconClass = "bi-lock", Controller = "Offboarding", Action = "RevokeAccess", Title = "Revoke IT & System Access", ModuleId = 10 },
    new SubModule { Id = 29, Name = "Final Payroll & Documents", IconClass = "bi-file-earmark-text", Controller = "Offboarding", Action = "FinalPayroll", Title = "Final Payroll & Document Handling", ModuleId = 10 },

    // HR Management
    new SubModule { Id = 30, Name = "Employee Records", IconClass = "bi-file-earmark-person", Controller = "HR", Action = "Records", Title = "Manage Employee Records", ModuleId = 11 },
    new SubModule { Id = 31, Name = "Leave Management", IconClass = "bi-calendar-event", Controller = "HR", Action = "Leave", Title = "Manage Leaves & Absences", ModuleId = 11 },
    new SubModule { Id = 32, Name = "Payroll Processing", IconClass = "bi-cash-coin", Controller = "HR", Action = "Payroll", Title = "Automate Payroll Processing", ModuleId = 11 },

    // Performance Management
    new SubModule { Id = 33, Name = "Performance Reviews", IconClass = "bi-graph-up", Controller = "Performance", Action = "Reviews", Title = "Employee Performance Reviews", ModuleId = 12 },
    new SubModule { Id = 34, Name = "KPI Tracking", IconClass = "bi-bar-chart-line", Controller = "Performance", Action = "KPIs", Title = "Track KPIs & Goals", ModuleId = 12 },
    new SubModule { Id = 35, Name = "Feedback & Recognition", IconClass = "bi-chat-left-text", Controller = "Performance", Action = "Feedback", Title = "360 Feedback & Recognition", ModuleId = 12 },

    // IT Service Desk
    new SubModule { Id = 36, Name = "Ticket Management", IconClass = "bi-ticket-detailed", Controller = "ITService", Action = "Tickets", Title = "Manage IT Support Tickets", ModuleId = 14 },
    new SubModule { Id = 37, Name = "System Monitoring", IconClass = "bi-speedometer", Controller = "ITService", Action = "Monitoring", Title = "Monitor IT Infrastructure", ModuleId = 14 },
    new SubModule { Id = 38, Name = "Hardware Inventory", IconClass = "bi-pc-display", Controller = "ITService", Action = "Inventory", Title = "Manage IT Assets", ModuleId = 14 },

    // Customer Support
    new SubModule { Id = 39, Name = "Support Tickets", IconClass = "bi-headset", Controller = "Support", Action = "Tickets", Title = "Manage Customer Tickets", ModuleId = 18 },
    new SubModule { Id = 40, Name = "Live Chat", IconClass = "bi-chat-dots", Controller = "Support", Action = "Chat", Title = "Provide Live Chat Support", ModuleId = 18 },
    new SubModule { Id = 41, Name = "FAQ & Help Center", IconClass = "bi-question-circle", Controller = "Support", Action = "FAQ", Title = "Manage Help Center Articles", ModuleId = 18 },

    // Analytics & Reporting
    new SubModule { Id = 42, Name = "Financial Reports", IconClass = "bi-file-earmark-bar-graph", Controller = "Reports", Action = "Finance", Title = "View Financial Reports", ModuleId = 20 },
    new SubModule { Id = 43, Name = "Employee Insights", IconClass = "bi-people", Controller = "Reports", Action = "Employees", Title = "Analyze Employee Performance", ModuleId = 20 },
    new SubModule { Id = 44, Name = "Sales & Revenue", IconClass = "bi-graph-up", Controller = "Reports", Action = "Sales", Title = "Track Sales & Revenue", ModuleId = 20 },
    new SubModule { Id = 45, Name = "Support Dashboard", IconClass = "bi-speedometer2", Controller = "Support", Action = "Index", Title = "View Support Overview", ModuleId = 18 },
    new SubModule { Id = 46, Name = "Orders Dashboard", IconClass = "bi-cart", Controller = "OrderReport", Action = "Index", Title = "View Orders Overview", ModuleId = 20 }
);

        }
    }
}