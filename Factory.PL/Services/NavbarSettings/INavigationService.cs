using Factory.PL.ViewModels.Admin;

namespace Factory.PL.Services.NavbarSettings
{
    public interface INavigationService
    {
        Task<List<NavbarLinkViewModel>> GetNavbarLinksAsync();

    }
}
