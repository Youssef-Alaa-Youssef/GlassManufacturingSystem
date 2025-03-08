using Factory.DAL.Models.Permission;

namespace Factory.PL.Services.Permssions
{
    public interface IModuleService
    {
        Task<List<Module>> GetModulesForUserAsync(string userId);
    }
}
