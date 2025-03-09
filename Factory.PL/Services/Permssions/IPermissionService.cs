using System.Security.Claims;

namespace Factory.PL.Services.Permssions
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, string controllerName, string actionName);
    }
}
