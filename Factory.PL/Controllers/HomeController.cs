using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Factory.DAL.Models.Home;
using Factory.PL.ViewModels;
using Factory.PL.ViewModels.Home;
using System.Diagnostics;
using Factory.BLL.InterFaces;

namespace Factory.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var Partners = await _unitOfWork.GetRepository<Partner>().GetAllAsync();
            //return View(Partners);
            return RedirectToAction("Login","Auth");
        }

        public async Task<IActionResult> FAQ()
        {
            var faqs = await _unitOfWork.GetRepository<FAQS>().GetAllAsync();
            //return View(faqs);
            return RedirectToAction("Login", "Auth");

        }

        public IActionResult AboutUs()
        {
            //return View();
            return RedirectToAction("Login", "Auth");

        }

        public IActionResult Contact()
        {
            //return View();
            return RedirectToAction("Login", "Auth");
        }
        public IActionResult UnderConstruction()
        {
            return View();
        }
        
        [Authorize(Roles = "SuperAdmin,Administrator")]
        [HttpGet]
        public IActionResult TeamMember()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamMemberAction(TeamMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(TeamMember), model);
            }

            var teamMember = new TeamMember
            {
                Name = model.Name,
                Role = model.Role,
                ProfileImageUrl = model.ProfileImageUrl,
                IconUrl = model.IconUrl,
                Email = model.Email,
                Phone = model.Phone,
                FacebookLink = model.FacebookLink,
                TwitterLink = model.TwitterLink,
                LinkedInLink = model.LinkedInLink,
                InstagramLink = model.InstagramLink,
                YouTubeLink = model.YouTubeLink,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                IsHidden = false,
                IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""
            };

            await _unitOfWork.GetRepository<TeamMember>().AddAsync(teamMember);
            await _unitOfWork.SaveChangesAsync();

            TempData["Success"] = "Team member details have been successfully saved.";
            return RedirectToAction(nameof(TeamMember));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Administrator")]
        public async Task<IActionResult> TeamMemberRep()
        {
            var teamMembers = (await _unitOfWork.GetRepository<TeamMember>().GetAllAsync()).ToList();

            var viewModel = new TeamMemberRepViewModel
            {
                TeamMembers = teamMembers
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", contact);
            }

            try
            {
                var ipAddress = GetClientIpAddress(HttpContext);

                var contactEntity = new ContactUs
                {
                    Name = contact.Name,
                    Email = contact.Email,
                    Message = contact.Message,
                    IpAddress = ipAddress,
                    CreatedDate = DateTime.UtcNow 
                };

                await _unitOfWork.GetRepository<ContactUs>().AddAsync(contactEntity);
                await _unitOfWork.SaveChangesAsync();
                TempData["Success"] = "Thank you for reaching out! Your message has been successfully sent, and we will get back to you shortly.";
                return RedirectToAction(nameof(contact));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, $"An error occurred while processing your request. {ex.Message} Please try again later.");

                return View("Contact", contact);
            }
        }
        [Authorize(Roles = "SuperAdmin,Administrator")]

        public async Task<IActionResult> ContactReport()
        {
            var contacts = (await _unitOfWork.GetRepository<ContactUs>().GetAllAsync()).ToList();

            var now = DateTime.Now;
            var startOfYear = new DateTime(now.Year, 1, 1);
            var contactStats = new List<ContactStats>
{
    new ContactStats { Category = "Contacts This Today", Count = contacts.Count(c => c.CreatedDate.Date == now.Date) },
            new ContactStats { Category = "Contacts This Month", Count = contacts.Count(c => c.CreatedDate.Year == now.Year && c.CreatedDate.Month == now.Month) },
    new ContactStats { Category = "Contacts This Year", Count = contacts.Count(c => c.CreatedDate.Year == now.Year) },
        new ContactStats { Category = "Total Contacts", Count = contacts.Count },

};



            var viewModel = new ContactUsDashboardViewModel
            {
                Contacts = contacts,
                ContactStats = contactStats
            };

            return View(viewModel);
        }
        [Authorize]
        public async Task<IActionResult> DashBoard()
        {
            var userCount = (await _unitOfWork.GetRepository<IdentityUser>().GetAllAsync()).Count();
            var teamMemberCount = (await _unitOfWork.GetRepository<TeamMember>().GetAllAsync()).Count();
            var contactCount = (await _unitOfWork.GetRepository<ContactUs>().GetAllAsync()).Count();
            var roleCount = (await _unitOfWork.GetRepository<IdentityRole>().GetAllAsync()).Count();

            var viewModel = new AdminDashboardViewModel
            {
                UserCount = userCount,
                TeamMemberCount = teamMemberCount,
                ContactCount = contactCount,
                RoleCount = roleCount
            };

            return View(viewModel);

        }

        public IActionResult PropertyDetails()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private string GetClientIpAddress(HttpContext context)
        {
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',').FirstOrDefault()?.Trim() ?? string.Empty;
            }

            return context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        }

    }
}
