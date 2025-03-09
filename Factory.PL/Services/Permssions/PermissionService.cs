//using Factory.BLL.InterFaces;
//using Factory.DAL.Models.Auth;
//using Factory.DAL.Models.Permission;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//namespace Factory.PL.Services.Permssions
//{
//    public class PermissionService : IPermissionService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public PermissionService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
//        {
//            _unitOfWork = unitOfWork;
//            _userManager = userManager;
//        }

//        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, string controllerName, string actionName)
//        {
//            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (string.IsNullOrEmpty(userId))
//            {
//                return false; // User is not authenticated
//            }

//            // Get the user's roles
//            var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));
//            if (userRoles == null || userRoles.Count == 0)
//            {
//                return false; // User has no roles
//            }

//            var permissionMapping = await _unitOfWork.GetRepository<RolePermission>()
//                .GetFirstOrDefaultAsync(pm => pm.ControllerName == controllerName && pm.ActionName == actionName);

//            if (permissionMapping == null)
//            {
//                return false; 
//            }

//            var permission = await _unitOfWork.GetRepository<PermissionTyepe>()
//                .GetFirstOrDefaultAsync(p => p.Name == permissionMapping.);

//            if (permission == null)
//            {
//                return false; 
//            }

//            var rolePermissions = await _unitOfWork.GetRepository<RolePermission>()
//                .Query()
//                .Include(rp => rp.Role) // Include the Role navigation property
//                .Where(rp => userRoles.Contains(rp.Role.Name) &&
//                             rp.PermissionId == permission.Id )
//                .ToListAsync();

//            return rolePermissions.Any(); // Return true if the user has the required permission
//        }
//    }
//}