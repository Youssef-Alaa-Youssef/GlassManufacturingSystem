using Factory.DAL.Models.Auth;
using Factory.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
    //[Authorize(Policy = "Role Management_Create")]
    //[Authorize(Policy = "Role Management_Update")]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<ApplicationUserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userModel = new ApplicationUserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Roles = roles.ToList()
                };
                userViewModels.Add(userModel);
            }

            ViewBag.Roles = _roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                })
                .ToList();

            return View(userViewModels);
        }

        public async Task<IActionResult> AllRoles(string query)
        {
            IQueryable<IdentityRole> rolesQuery = _roleManager.Roles;

            if (!string.IsNullOrEmpty(query))
            {
                rolesQuery = rolesQuery.Where(r => (r.Name ?? "").Contains(query));
            }

            var roles = await rolesQuery.ToListAsync();
            ViewBag.query = query;

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new ApplicationUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName ?? string.Empty,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            };

            ViewBag.Roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRoles(ApplicationUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (model.Roles == null || !model.Roles.Any())
            {
                await _userManager.UpdateAsync(user);
                TempData["Success"] = "All roles have been removed from the user.";
                return RedirectToAction(nameof(Index));
            }

            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var rolesToAdd = model.Roles;
            var result = await _userManager.AddToRolesAsync(user, rolesToAdd);
            //await _signInManager.RefreshSignInAsync(user);


            if (result.Succeeded)
            {
                TempData["Success"] = "Roles assigned successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Error assigning roles.";
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST action to create a new role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationRoles role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Role created successfully.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Error"] = "Error creating role.";
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(role);
        }

        // GET action to edit a role
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var roleViewModel = new ApplicationRolesViewModel
            {
                Id = role.Id,
                Name = role.Name ?? string.Empty
            };

            return View(roleViewModel);
        }

        // POST action to update a role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationRolesViewModel role)
        {
            if (id != role.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingRole = await _roleManager.FindByIdAsync(id);
                if (existingRole == null) return NotFound();

                existingRole.Name = role.Name;

                var result = await _roleManager.UpdateAsync(existingRole);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Role updated successfully.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Error"] = "Error updating role.";
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(role);
        }

        // GET action to delete a role
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            return View(role);
        }

        // POST action to confirm and delete a role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return NotFound();

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name ?? string.Empty);
            if (usersInRole.Any())
            {
                TempData["Error"] = "Cannot delete role with associated users.";
                return RedirectToAction(nameof(AllRoles)); // Adjust to your list view action
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                TempData["Success"] = "Role deleted successfully.";
                return RedirectToAction(nameof(Index)); // Adjust to your main view action
            }

            TempData["Error"] = "Error deleting role.";
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(role);
        }
    }
}
