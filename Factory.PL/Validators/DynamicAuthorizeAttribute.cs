using Factory.PL.Services.Permssions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class DynamicAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;
        var routeData = httpContext.GetRouteData();

        var controllerName = routeData.Values["controller"]?.ToString();
        var actionName = routeData.Values["action"]?.ToString();

        if (string.IsNullOrEmpty(controllerName) || string.IsNullOrEmpty(actionName))
        {
            context.Result = new ForbidResult(); 
            return;
        }

        var permissionService = httpContext.RequestServices.GetService<IPermissionService>();
        if (permissionService == null)
        {
            context.Result = new ForbidResult(); 
            return;
        }

        var hasPermission = await permissionService.HasPermissionAsync(httpContext.User, controllerName, actionName);
        if (!hasPermission)
        {
            context.Result = new ForbidResult();
            return;
        }

        Policy = $"{controllerName}_{actionName}";
    }
}