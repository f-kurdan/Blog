using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userMgr;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userMgr)
        {
            _signInManager = signInManager;
            _userMgr = userMgr;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (!result.Succeeded)
            {
                return View(vm);
            }

            var user = await _userMgr.FindByNameAsync(vm.UserName);
            var isAdmin = await _userMgr.IsInRoleAsync(user, "Admin");
            if (isAdmin)
            {
                return RedirectToAction("Index", "Panel");
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = new IdentityUser()
            {
                UserName = vm.Email,
                Email = vm.Email
            };

            var result = await _userMgr.CreateAsync(user, "password");
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
            //return RedirectToAction("Index", "Panel");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Panel");
        }
    }
}