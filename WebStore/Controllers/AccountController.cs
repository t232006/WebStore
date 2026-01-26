using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Identity;

namespace WebStore.Controllers
{
    public class AccountController :Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signinManager;
        private readonly Logger<AccountController> logger;

        public AccountController(
            UserManager<User> _userManager, 
            SignInManager<User> _signinManager,
            Logger<AccountController> logger)
        {
            userManager = _userManager;
            signinManager = _signinManager;
            this.logger = logger;
        }
        IActionResult Register() => View();
        IActionResult Login() => View();
        IActionResult Logout() => View();
        IActionResult AccessDenied() => View();


    }
}
