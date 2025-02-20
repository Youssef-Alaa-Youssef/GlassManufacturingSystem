using Factory.PL.ViewModels.Settings;

namespace Factory.PL.Services.Setting
{
    public interface ISettingsService
    {
        Task<NotificationSettingsViewModel> GetNotificationSettingsAsync();
        Task SaveNotificationSettingsAsync(NotificationSettingsViewModel model);
        Task<ContractSettingsViewModel> GetContractSettingsAsync();
        Task SaveContractSettingsAsync(ContractSettingsViewModel model);
    }
}
