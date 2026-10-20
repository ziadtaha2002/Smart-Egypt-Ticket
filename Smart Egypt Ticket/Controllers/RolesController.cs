using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Smart_Egypt_Ticket.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(String RoleName)
        {
            if(string.IsNullOrWhiteSpace(RoleName))
            {
                ModelState.AddModelError("RoleName", "Role name is required.");
                return View();
            }

            bool isExsist = await _roleManager.RoleExistsAsync(RoleName);
            if (isExsist)
            {
                ModelState.AddModelError("RoleName","Role name is already exist");
                return View();
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(RoleName));
                if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }



    }
}
