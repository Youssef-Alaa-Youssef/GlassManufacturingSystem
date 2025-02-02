using Factory.DAL.Models.Permission;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;


namespace Factory.BLL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Generic Methods
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");
        Task<TEntity> GetByIdAsync(int id, string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> RemoveAsync(TEntity entity);

        // PermissionTyepe-Specific Methods
        Task<List<PermissionTyepe>> GetAllPermissionTyepeAsync();
        Task<PermissionTyepe> GetPermissionTyepeByIdAsync(int id, string includeProperties = "");
        Task<PermissionTyepe> AddPermissionTyepeAsync(PermissionTyepe PermissionTyepe);
        Task<PermissionTyepe> UpdatePermissionTyepeAsync(PermissionTyepe PermissionTyepe);
        Task<PermissionTyepe> RemovePermissionTyepeAsync(PermissionTyepe PermissionTyepe);

        // Module-Specific Methods
        Task<List<Module>> GetAllModulesAsync();
        Task<Module> GetModuleByIdAsync(int id, string includeProperties = "");
        Task<Module> AddModuleAsync(Module module);
        Task<Module> UpdateModuleAsync(Module module);
        Task<Module> RemoveModuleAsync(Module module);

        // Policy Registration
        Task RegisterPoliciesAsync(AuthorizationOptions options);

        // Add RemoveRange method
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        Task<List<Module>> GetModulesForUserAsync(string userId);

    }
}