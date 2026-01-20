using cctvtemplate.Models;
using cctvtemplate.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cctvtemplate.Controllers
{
    public class AuthController(UserManager<AppUser> _userManager , SignInManager<AppUser> _signInManager , RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                ModelState.AddModelError("","Email or Password is wrong");
                return View(vm);

            }

            var checkPass = await _signInManager.PasswordSignInAsync(user, vm.Password,false , true);

            if(!checkPass.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is wrong");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUser newUser = new()
            {
                FullName = vm.FullName,
                Email = vm.Email,
                UserName = vm.Username
            };

            var result = await _userManager.CreateAsync(newUser,vm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(vm);
            }

            await _userManager.AddToRoleAsync(newUser, "Member");

            await _signInManager.SignInAsync(newUser, false);

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
