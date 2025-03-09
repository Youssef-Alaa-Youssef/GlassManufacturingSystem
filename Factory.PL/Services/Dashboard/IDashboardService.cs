using Factory.PL.ViewModels.Home.Dashboard;

namespace Factory.PL.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<AdminDashboardViewModel> GetDashboardDataAsync();
    }
}
