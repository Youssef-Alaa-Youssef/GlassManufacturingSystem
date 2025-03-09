namespace Factory.PL.ViewModels.Home.Dashboard
{
    public class DashboardItem
    {
        public string Title { get; set; } = string.Empty;
        public int Value { get; set; }
        public string IconClass { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TrendText { get; set; } = string.Empty;
        public string TrendClass { get; set; } = string.Empty;
        public string TrendIcon { get; set; } = string.Empty;
        public string ChartId { get; set; } = string.Empty;
        public string ChartTitle { get; set; } = string.Empty;
    }
}