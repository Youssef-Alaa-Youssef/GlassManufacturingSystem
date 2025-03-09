namespace Factory.PL.ViewModels.Home.Dashboard
{
    public class AdminDashboardViewModel
    {
        public List<DashboardItem> DashboardItems { get; set; } = new List<DashboardItem>();

        public List<RecentActivity> RecentActivities { get; set; } = new List<RecentActivity>();

        public List<QuickAction> QuickActions { get; set; } = new List<QuickAction>();

        public decimal FinancialSummary { get; set; }
        public int UserCount { get; set; }
        public int TeamMemberCount { get; set; }
        public int RoleCount { get; set; }
        public int ContactCount { get; set; }
        public int InvoiceCount { get; set; }
        public decimal Expenses { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal Profit { get; set; }

    }
}