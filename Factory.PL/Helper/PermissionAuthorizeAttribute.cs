using Microsoft.AspNetCore.Authorization;

namespace Factory.PL.Helper
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(string permission, string module)
        {
            Policy = $"{permission}_{module}";
        }
    }
}
