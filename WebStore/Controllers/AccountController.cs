using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Identity;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController :Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signinManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(
            UserManager<User> _userManager, 
            SignInManager<User> _signinManager,
            ILogger<AccountController> logger)
        {
            userManager = _userManager;
            signinManager = _signinManager;
            this.logger = logger;
        }
        public IActionResult Register() => View(new RegisterUserViewModel());
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);
            var user = new User { UserName = Model.UserName };
            var regResult = await userManager.CreateAsync(user, Model.password);
            if (regResult.Succeeded)
            {
                logger.LogInformation("User {0} has registrated", user);
                user.regData = DateTime.Today;
                await signinManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in regResult.Errors)
                ModelState.AddModelError("", error.Description);
            logger.LogWarning(string.Join(", ", regResult.Errors.Select(e=>e.Description)));
            return View(Model);

        }
        public IActionResult Login(string? _redirect) => View(new LoginUserViewModel {redirectUrl=_redirect });
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);
            var signResult = await signinManager.PasswordSignInAsync(
                Model.login,
                Model.password,
                Model.rememberMe,
                lockoutOnFailure: false);
            if (signResult.Succeeded)
            {
                logger.LogInformation("User {0} has entered", Model.login);
                return LocalRedirect(Model.redirectUrl ?? "/");
            }
            ModelState.AddModelError("", "Username or password is incorrect");
            logger.LogWarning("Username {0} has failed in trying to enter", Model.login);
            return View(Model);
        }
        public async Task<IActionResult> Logout() 
        {
            var username = User.Identity!.Name;
            await signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied(string? redirectUrl) 
        {
            ViewBag.redirectUrl = redirectUrl;
            return View();
            
        }


    }
}
