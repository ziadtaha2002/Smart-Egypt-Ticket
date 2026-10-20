using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Smart_Egypt_Ticket.DTO;
using Smart_Egypt_Ticket.Models;

namespace Smart_Egypt_Ticket.Controllers
{
    public class AccountController : Controller
    {
        // User used for CRUD operations on the user entity 
        private readonly UserManager<ApplicationUser> _userManager;
        // SignInManager used for adding cookie to the user and signing in the user
        private readonly SignInManager<ApplicationUser> _signInManager;
        // RoleManager used for CRUD operations on the role entity
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = new ApplicationUser()
                {
                    UserName = registerUserDTO.Email,
                    Email = registerUserDTO.Email,
                    FullName = registerUserDTO.FullName,
                    CreatedAtUtc = DateTime.UtcNow,
                    IsActive = true
                };

                IdentityResult result = await _userManager.CreateAsync(AppUser, registerUserDTO.Password);
                if (result.Succeeded)
                {
                    //the first user to register will be assigned the Admin role,
                    //this is done by checking if the email of the user is equal to the email of the admin,
                    //if it is then the user will be assigned the Admin role
                    if (string.Equals(registerUserDTO.Email, "Ziad@Admin.com", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        }

                        await _userManager.AddToRoleAsync(AppUser, "Admin");
                    }

                    //sign in the user after successful registration
                    //cookie will be added to the user and the user will be signed in
                        //without the need to login again
                    await _signInManager.SignInAsync(AppUser, isPersistent: false);
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View("Register", registerUserDTO);


        }
        
        //public async Task<IActionResult> Register(RegisterUserDTO model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = new ApplicationUser
        //    {
        //        UserName = model.Email,
        //        Email = model.Email,
        //        FullName = model.FullName,
        //    };
            
        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError(string.Empty, error.Description);
        //    }

        //    return View(model); 
        //}

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        //ValidateAntiForgeryToken is used to prevent CSRF attacks, it checks if the request is coming from the same site or not
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}