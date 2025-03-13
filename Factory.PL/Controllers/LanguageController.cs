using Factory.PL.Helper;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Factory.PL.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            try
            {
                // Validate the culture parameter
                if (string.IsNullOrWhiteSpace(culture))
                {
                    throw new ArgumentException("Culture cannot be null or empty.", nameof(culture));
                }

                if (string.IsNullOrWhiteSpace(returnUrl) || !Url.IsLocalUrl(returnUrl))
                {
                    throw new ArgumentException("Invalid return URL.", nameof(returnUrl));
                }

                // Set the culture cookie
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddYears(1),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    }
                );

                return LocalRedirect(returnUrl);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}