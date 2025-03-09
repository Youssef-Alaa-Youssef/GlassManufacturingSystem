using Factory.BLL.Interfaces;
using Factory.BLL.InterFaces;
using Factory.DAL.Enums;
using Factory.DAL.Models.Auth;
using Factory.DAL.Models.Home;
using Factory.PL.ViewModels.Home.Dashboard;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Factory.PL.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AdminDashboardViewModel> GetDashboardDataAsync()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var dashboardItems = new List<DashboardItem>();
            var recentActivities = new List<RecentActivity>();
            var quickActions = new List<QuickAction>();

            decimal financialSummary = 0;
            int invoiceCount = 0;
            decimal expenses = 0;
            decimal profit = 0;

            // Fetch common data
            var userCount = (await _unitOfWork.GetRepository<ApplicationUser>().GetAllAsync()).Count();
            var teamMemberCount = (await _unitOfWork.GetRepository<TeamMember>().GetAllAsync()).Count();
            var contactCount = (await _unitOfWork.GetRepository<ContactUs>().GetAllAsync()).Count();
            var roleCount = (await _unitOfWork.GetRepository<IdentityRole>().GetAllAsync()).Count();

            // Role-specific data
            switch (userRole)
            {
                case nameof(UserRole.SuperAdmin):
                case nameof(UserRole.Owner):
                case nameof(UserRole.GM):
                    dashboardItems = new List<DashboardItem>
                    {
                        new DashboardItem
                        {
                            Title = "Users",
                            Value = userCount,
                            IconClass = "bi-people-fill",
                            Description = "Active users in system",
                            TrendText = "12% from last month",
                            TrendClass = "text-success",
                            TrendIcon = "bi-arrow-up",
                            ChartId = "UserCount",
                            ChartTitle = "Users Over Time"
                        },
                        new DashboardItem
                        {
                            Title = "Team Members",
                            Value = teamMemberCount,
                            IconClass = "bi-person-badge-fill",
                            Description = "Managed team members",
                            TrendText = "5% from last month",
                            TrendClass = "text-danger",
                            TrendIcon = "bi-arrow-down",
                            ChartId = "TeamMemberCount",
                            ChartTitle = "Team Members Over Time"
                        },
                        new DashboardItem
                        {
                            Title = "Contacts",
                            Value = contactCount,
                            IconClass = "bi-envelope-fill",
                            Description = "Recent communications",
                            TrendText = "8% from last month",
                            TrendClass = "text-success",
                            TrendIcon = "bi-arrow-up",
                            ChartId = "ContactCount",
                            ChartTitle = "Contacts Over Time"
                        },
                        new DashboardItem
                        {
                            Title = "Roles",
                            Value = roleCount,
                            IconClass = "bi-shield-lock-fill",
                            Description = "System access roles",
                            TrendText = "3% from last month",
                            TrendClass = "text-success",
                            TrendIcon = "bi-arrow-up",
                            ChartId = "RoleCount",
                            ChartTitle = "Roles Over Time"
                        }
                    };

                    recentActivities = new List<RecentActivity>
                    {
                        new RecentActivity
                        {
                            Title = "New user registered",
                            Time = "2 mins ago",
                            Description = "John Doe joined the system"
                        },
                        new RecentActivity
                        {
                            Title = "System update",
                            Time = "15 mins ago",
                            Description = "Version 2.3.1 deployed"
                        }
                    };

                    quickActions = new List<QuickAction>
                    {
                        new QuickAction
                        {
                            Title = "Add New User",
                            IconClass = "bi-person-plus"
                        },
                        new QuickAction
                        {
                            Title = "Create Report",
                            IconClass = "bi-file-earmark-plus"
                        }
                    };
                    break;

                case nameof(UserRole.Accountant):
                    financialSummary = 100000;
                    invoiceCount = 25;
                    expenses = 50000;
                    profit = 50000;

                    dashboardItems = new List<DashboardItem>
                    {
                        new DashboardItem
                        {
                            Title = "Financial Summary",
                            Value = (int)financialSummary,
                            IconClass = "bi-cash-coin",
                            Description = "Total revenue this month",
                            TrendText = "10% from last month",
                            TrendClass = "text-success",
                            TrendIcon = "bi-arrow-up",
                            ChartId = "FinancialSummary",
                            ChartTitle = "Financial Summary Over Time"
                        },
                        new DashboardItem
                        {
                            Title = "Invoices",
                            Value = invoiceCount,
                            IconClass = "bi-receipt",
                            Description = "Invoices generated this month",
                            TrendText = "5% from last month",
                            TrendClass = "text-danger",
                            TrendIcon = "bi-arrow-down",
                            ChartId = "InvoiceCount",
                            ChartTitle = "Invoices Over Time"
                        },
                        new DashboardItem
                        {
                            Title = "Expenses",
                            Value = (int)expenses,
                            IconClass = "bi-currency-dollar",
                            Description = "Total expenses this month",
                            TrendText = "8% from last month",
                            TrendClass = "text-success",
                            TrendIcon = "bi-arrow-up",
                            ChartId = "Expenses",
                            ChartTitle = "Expenses Over Time"
                        },
                        new DashboardItem
                        {
                            Title = "Profit",
                            Value = (int)profit,
                            IconClass = "bi-graph-up",
                            Description = "Net profit this month",
                            TrendText = "15% from last month",
                            TrendClass = "text-success",
                            TrendIcon = "bi-arrow-up",
                            ChartId = "Profit",
                            ChartTitle = "Profit Over Time"
                        }
                    };

                    recentActivities = new List<RecentActivity>
                    {
                        new RecentActivity
                        {
                            Title = "Invoice Paid",
                            Time = "2 mins ago",
                            Description = "Invoice #1234 paid by John Doe"
                        },
                        new RecentActivity
                        {
                            Title = "Expense Recorded",
                            Time = "15 mins ago",
                            Description = "Office supplies purchase recorded"
                        }
                    };

                    quickActions = new List<QuickAction>
                    {
                        new QuickAction
                        {
                            Title = "Create Invoice",
                            IconClass = "bi-file-earmark-plus"
                        },
                        new QuickAction
                        {
                            Title = "Record Payment",
                            IconClass = "bi-cash-coin"
                        }
                    };
                    break;

                case nameof(UserRole.Sales):
                    dashboardItems = new List<DashboardItem>
                    {
                        new DashboardItem
                        {
                            Title = "Leads",
                            Value = 50,
                            IconClass = "bi-person-lines-fill",
                            Description = "Active leads in system",
                            TrendText = "20% from last month",
                            TrendClass = "text-success",
                            TrendIcon = "bi-arrow-up",
                            ChartId = "Leads",
                            ChartTitle = "Leads Over Time"
                        },
                        new DashboardItem
                        {
                            Title = "Deals Closed",
                            Value = 10,
                            IconClass = "bi-check-circle-fill",
                            Description = "Deals closed this month",
                            TrendText = "15% from last month",
                            TrendClass = "text-success",
                            TrendIcon = "bi-arrow-up",
                            ChartId = "DealsClosed",
                            ChartTitle = "Deals Closed Over Time"
                        }
                    };

                    recentActivities = new List<RecentActivity>
                    {
                        new RecentActivity
                        {
                            Title = "New Lead Added",
                            Time = "2 mins ago",
                            Description = "John Doe added as a new lead"
                        },
                        new RecentActivity
                        {
                            Title = "Deal Closed",
                            Time = "15 mins ago",
                            Description = "Deal #1234 closed with Client A"
                        }
                    };

                    quickActions = new List<QuickAction>
                    {
                        new QuickAction
                        {
                            Title = "Add New Lead",
                            IconClass = "bi-person-plus"
                        },
                        new QuickAction
                        {
                            Title = "Create Proposal",
                            IconClass = "bi-file-earmark-plus"
                        }
                    };
                    break;

                default:
                    dashboardItems = new List<DashboardItem>();
                    recentActivities = new List<RecentActivity>();
                    quickActions = new List<QuickAction>();
                    break;
            }

            return new AdminDashboardViewModel
            {
                DashboardItems = dashboardItems,
                RecentActivities = recentActivities,
                QuickActions = quickActions,
                FinancialSummary = financialSummary,
                InvoiceCount = invoiceCount,
                Expenses = expenses,
                Profit = profit
            };
        }
    }
}