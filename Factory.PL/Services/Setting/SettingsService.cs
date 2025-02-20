using Factory.BLL.InterFaces;
using Factory.DAL.Enums;
using Factory.DAL.Models.Settings;
using Factory.PL.ViewModels.Settings;

namespace Factory.PL.Services.Setting
{
    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SettingsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NotificationSettingsViewModel> GetNotificationSettingsAsync()
        {
            var settings = (await _unitOfWork.GetRepository<NotificationSettings>().GetAllAsync()).FirstOrDefault();
            return new NotificationSettingsViewModel
            {
                EnableEmailNotifications = settings?.EnableEmailNotifications ?? false,
                EnableSmsNotifications = settings?.EnableSmsNotifications ?? false,
                EnablePushNotifications = settings?.EnablePushNotifications ?? false,
                EmailAddress = settings?.EmailAddress,
                PhoneNumber = settings?.PhoneNumber,
                Frequency = settings?.Frequency ?? NotificationFrequency.Daily
            };
        }

        public async Task SaveNotificationSettingsAsync(NotificationSettingsViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var settings = (await _unitOfWork.GetRepository<NotificationSettings>().GetAllAsync()).FirstOrDefault();
            if (settings == null)
            {
                settings = new NotificationSettings();
                await _unitOfWork.GetRepository<NotificationSettings>().AddAsync(settings);
            }

            settings.EnableEmailNotifications = model.EnableEmailNotifications;
            settings.EnableSmsNotifications = model.EnableSmsNotifications;
            settings.EnablePushNotifications = model.EnablePushNotifications;
            settings.EmailAddress = model.EmailAddress;
            settings.PhoneNumber = model.PhoneNumber;
            settings.Frequency = model.Frequency;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ContractSettingsViewModel> GetContractSettingsAsync()
        {
            var settings = (await _unitOfWork.GetRepository<ContractSettings>().GetAllAsync()).FirstOrDefault();
            return new ContractSettingsViewModel
            {
                ContractStartDate = settings?.ContractStartDate ?? DateTime.Now,
                ContractEndDate = settings?.ContractEndDate ?? DateTime.Now.AddYears(1),
                IsActive = settings?.IsActive ?? true
            };
        }

        public async Task SaveContractSettingsAsync(ContractSettingsViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var settings = (await _unitOfWork.GetRepository<ContractSettings>().GetAllAsync()).FirstOrDefault();
            if (settings == null)
            {
                settings = new ContractSettings();
                await _unitOfWork.GetRepository<ContractSettings>().AddAsync(settings);
            }

            settings.ContractStartDate = model.ContractStartDate;
            settings.ContractEndDate = model.ContractEndDate;
            settings.IsActive = model.IsActive;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
