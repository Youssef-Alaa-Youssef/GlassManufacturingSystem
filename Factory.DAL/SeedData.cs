using Factory.DAL.Models.Permission;
using Microsoft.EntityFrameworkCore;
namespace Factory.DAL
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedModules(modelBuilder);
            SeedSubModules(modelBuilder);
            SeedPages(modelBuilder);
        }

        private static void SeedModules(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>().HasData(
                new Module { Id = 1, Name = "User Management", IconClass = "bi-people", IsActive = true },
                new Module { Id = 2, Name = "Role Management", IconClass = "bi-person-badge", IsActive = true },
                new Module { Id = 3, Name = "Inventory Management", IconClass = "bi-box-seam", IsActive = true },
                new Module { Id = 4, Name = "Order Management", IconClass = "bi-cart", IsActive = true },
                new Module { Id = 5, Name = "Payroll Management", IconClass = "bi-cash-stack", IsActive = true },
                new Module { Id = 6, Name = "System Management", IconClass = "bi-speedometer", IsActive = true },
                new Module { Id = 7, Name = "Support Management", IconClass = "bi-headset", IsActive = true },
                new Module { Id = 8, Name = "Reports", IconClass = "bi-file-earmark-bar-graph", IsActive = true },
                new Module { Id = 9, Name = "Settings", IconClass = "bi-gear", IsActive = true }
            );
        }

        private static void SeedSubModules(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubModule>().HasData(
                new SubModule { Id = 1, Name = "Users", IconClass = "bi-people", ModuleId = 1 },
                new SubModule { Id = 2, Name = "Permissions", IconClass = "bi-shield-lock", ModuleId = 1 },
                new SubModule { Id = 3, Name = "Roles", IconClass = "bi-person-badge", ModuleId = 2 },
                new SubModule { Id = 4, Name = "Assign Roles", IconClass = "bi-person-check", ModuleId = 2 },
                new SubModule { Id = 5, Name = "Inventory", IconClass = "bi-box-seam", ModuleId = 3 },
                new SubModule { Id = 6, Name = "Stock Levels", IconClass = "bi-boxes", ModuleId = 3 },
                new SubModule { Id = 7, Name = "New Order", IconClass = "bi-cart-plus", ModuleId = 4 },
                new SubModule { Id = 8, Name = "Order History", IconClass = "bi-cart-check", ModuleId = 4 },
                new SubModule { Id = 9, Name = "Payroll Dashboard", IconClass = "bi-cash-stack", ModuleId = 5 },
                new SubModule { Id = 10, Name = "Employee Records", IconClass = "bi-file-earmark-person", ModuleId = 5 },
                new SubModule { Id = 11, Name = "Tickets", IconClass = "bi-ticket-detailed", ModuleId = 6 },
                new SubModule { Id = 12, Name = "System Monitoring", IconClass = "bi-speedometer", ModuleId = 6 },
                new SubModule { Id = 13, Name = "Support Tickets", IconClass = "bi-headset", ModuleId = 7 },
                new SubModule { Id = 14, Name = "Live Chat", IconClass = "bi-chat-dots", ModuleId = 7 },
                new SubModule { Id = 15, Name = "Reports", IconClass = "bi-file-earmark-bar-graph", ModuleId = 8 },
                new SubModule { Id = 16, Name = "Dashboards", IconClass = "bi-speedometer2", ModuleId = 8 },
                new SubModule { Id = 17, Name = "General Settings", IconClass = "bi-gear", ModuleId = 9 },
                new SubModule { Id = 18, Name = "Security", IconClass = "bi-shield", ModuleId = 9 }
            );
        }

        private static void SeedPages(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>().HasData(
                new Page { Id = 1, Name = "Permission List", Action = "Index", Controller = "PermissionManagement", IsActive = true, SubmoduleId = 2,SecureUrlKey= GenerateSecureKey() },
                new Page { Id = 2, Name = "Assign Permission", Action = "AssignPermissions", Controller = "PermissionManagement", IsActive = true, SubmoduleId = 2, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 3, Name = "Module List", Action = "Index", Controller = "Module", IsActive = true, SubmoduleId = 3, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 4, Name = "Add Module", Action = "Create", Controller = "Module", IsActive = true, SubmoduleId = 3, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 5, Name = "Submodule List", Action = "Index", Controller = "SubModule", IsActive = true, SubmoduleId = 4, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 6, Name = "Add Submodule", Action = "Create", Controller = "SubModule", IsActive = true, SubmoduleId = 4, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 7, Name = "User List", Action = "Index", Controller = "Auth", IsActive = true, SubmoduleId = 1, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 8, Name = "Add User", Action = "Create", Controller = "Auth", IsActive = true, SubmoduleId = 1, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 9, Name = "Role List", Action = "Index", Controller = "Role", IsActive = true, SubmoduleId = 3, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 10, Name = "Add Role", Action = "Create", Controller = "Role", IsActive = true, SubmoduleId = 3, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 11, Name = "Warehouse List", Action = "Index", Controller = "Warehouse", IsActive = true, SubmoduleId = 5, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 12, Name = "Add Warehouse", Action = "Create", Controller = "Warehouse", IsActive = true, SubmoduleId = 5, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 13, Name = "Item List", Action = "Index", Controller = "Item", IsActive = true, SubmoduleId = 6 , SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 14, Name = "Add Item", Action = "Create", Controller = "Item", IsActive = true, SubmoduleId = 6, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 15, Name = "Create Order", Action = "Create", Controller = "Order", IsActive = true, SubmoduleId = 7, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 16, Name = "Order List", Action = "Index", Controller = "Order", IsActive = true, SubmoduleId = 8, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 17, Name = "General Settings", Action = "General", Controller = "Settings", IsActive = true, SubmoduleId = 17, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 18, Name = "Security Settings", Action = "Security", Controller = "Settings", IsActive = true, SubmoduleId = 18, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 19, Name = "Payroll Dashboard", Action = "Index", Controller = "Payroll", IsActive = true, SubmoduleId = 9, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 20, Name = "Employee Salaries", Action = "EmployeeSalaries", Controller = "Payroll", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 21, Name = "Salary Processing", Action = "ProcessSalaries", Controller = "Payroll", IsActive = true, SubmoduleId = 9, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 22, Name = "Payroll Reports", Action = "Reports", Controller = "Payroll", IsActive = true, SubmoduleId = 15, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 23, Name = "Bonuses Management", Action = "Bonuses", Controller = "Payroll", IsActive = true, SubmoduleId = 9, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 24, Name = "Deductions", Action = "Deductions", Controller = "Payroll", IsActive = true, SubmoduleId = 9, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 25, Name = "Tax Calculations", Action = "Tax", Controller = "Payroll", IsActive = true, SubmoduleId = 9, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 26, Name = "Payslip Generation", Action = "GeneratePayslip", Controller = "Payroll", IsActive = true, SubmoduleId = 9, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 27, Name = "Overtime Payments", Action = "Overtime", Controller = "Payroll", IsActive = true, SubmoduleId = 9, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 28, Name = "Payroll History", Action = "History", Controller = "Payroll", IsActive = true, SubmoduleId = 9, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 29, Name = "Financial Orders", Action = "Index", Controller = "Accountant", IsActive = true, SubmoduleId = 15, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 30, Name = "Pre-Onboarding", Action = "PreOnboarding", Controller = "Onboarding", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 31, Name = "IT Setup", Action = "ITSetup", Controller = "Onboarding", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 32, Name = "Training & Orientation", Action = "Training", Controller = "Onboarding", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 33, Name = "Exit Clearance", Action = "Clearance", Controller = "Offboarding", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 34, Name = "Access Revocation", Action = "RevokeAccess", Controller = "Offboarding", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 35, Name = "Final Payroll & Documents", Action = "FinalPayroll", Controller = "Offboarding", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 36, Name = "Employee Records", Action = "Records", Controller = "HR", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 37, Name = "Leave Management", Action = "Leave", Controller = "HR", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 38, Name = "Payroll Processing", Action = "Payroll", Controller = "HR", IsActive = true, SubmoduleId = 9, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 39, Name = "Performance Reviews", Action = "Reviews", Controller = "Performance", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 40, Name = "KPI Tracking", Action = "KPIs", Controller = "Performance", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 41, Name = "Feedback & Recognition", Action = "Feedback", Controller = "Performance", IsActive = true, SubmoduleId = 10, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 42, Name = "Ticket Management", Action = "Tickets", Controller = "ITService", IsActive = true, SubmoduleId = 11, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 43, Name = "System Monitoring", Action = "Monitoring", Controller = "ITService", IsActive = true, SubmoduleId = 12, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 44, Name = "Hardware Inventory", Action = "Inventory", Controller = "ITService", IsActive = true, SubmoduleId = 5, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 45, Name = "Support Tickets", Action = "Tickets", Controller = "Support", IsActive = true, SubmoduleId = 13, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 46, Name = "Live Chat", Action = "Chat", Controller = "Support", IsActive = true, SubmoduleId = 14, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 47, Name = "FAQ & Help Center", Action = "FAQ", Controller = "Support", IsActive = true, SubmoduleId = 13, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 48, Name = "Financial Reports", Action = "Finance", Controller = "Reports", IsActive = true, SubmoduleId = 15, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 49, Name = "Employee Insights", Action = "Employees", Controller = "Reports", IsActive = true, SubmoduleId = 15, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 50, Name = "Sales & Revenue", Action = "Sales", Controller = "Reports", IsActive = true, SubmoduleId = 15, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 51, Name = "Support Dashboard", Action = "Index", Controller = "Support", IsActive = true, SubmoduleId = 13, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 52, Name = "Orders Dashboard", Action = "Index", Controller = "OrderReport", IsActive = true, SubmoduleId = 8, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 53, Name = "Data Management", Action = "Settings", Controller = "ExportImport", IsActive = true, SubmoduleId = 17, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 54, Name = "Data Export", Action = "Export", Controller = "ExportImport", IsActive = true, SubmoduleId = 17, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 55, Name = "Data Import", Action = "Import", Controller = "ExportImport", IsActive = true, SubmoduleId = 17, SecureUrlKey = GenerateSecureKey() },
                new Page { Id = 56, Name = "Order Dashboard", Action = "Index", Controller = "OrderReport", IsActive = true, SubmoduleId = 16, SecureUrlKey = GenerateSecureKey() }
            );
        }
        private static string GenerateSecureKey()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10);
        }

    }
}
