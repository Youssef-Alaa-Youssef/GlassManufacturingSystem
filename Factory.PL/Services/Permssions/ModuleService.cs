using Factory.BLL.InterFaces;
using Factory.DAL.Models.Permission;

namespace Factory.PL.Services.Permssions
{
    public interface IModuleService
    {
        Task<List<Module>> GetModulesForUserAsync(string userId);
    }

    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Module>> GetModulesForUserAsync(string userId)
        {
            return await _unitOfWork.GetRepository<Module>().GetModulesForUserAsync(userId);
        }
    }

}
