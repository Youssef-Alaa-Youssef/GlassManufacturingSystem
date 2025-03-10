using Factory.DAL.Models.Permission;

namespace Factory.PL.Helper
{
    public static class UrlHelper
    {
        public static string GetSecureUrl(Page page)
        {
            if (string.IsNullOrEmpty(page.SecureUrlKey))
            {
                return $"/{page.Controller}/{page.Action}";
            }

            return $"/s/{page.SecureUrlKey}";
        }
    }
}
