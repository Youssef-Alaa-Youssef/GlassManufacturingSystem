using Microsoft.Extensions.Caching.Memory;
using Factory.DAL.Models.Permission;
using Factory.BLL.InterFaces;
using Factory.PL.Services.Permssions;

namespace Factory.PL.Services.Permissions
{
    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKeyPrefix = "UserModules_"; 

        public ModuleService(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        public async Task<List<Module>> GetModulesForUserAsync(string userId)
        {
            var cacheKey = $"{CacheKeyPrefix}{userId}";

            if (_memoryCache.TryGetValue(cacheKey, out List<Module> cachedModules))
            {
                return cachedModules; 
            }

            var modules = await _unitOfWork.GetRepository<Module>().GetModulesForUserAsync(userId);
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10)); 

            _memoryCache.Set(cacheKey, modules, cacheOptions);

            return modules;
        }
    }
}