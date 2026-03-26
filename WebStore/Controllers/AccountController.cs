using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebStore.Domain.Identity;
using WebStore.Mapping;
using WebStore.Services.InSQL;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController :Controller
    {
        private readonly IStaffData<User, string> user;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signinManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(
            IStaffData<User,string> _user,
            UserManager<User> _userManager, 
            SignInManager<User> _signinManager,
            ILogger<AccountController> logger)
        {
            user = _user;
            userManager = _userManager;
            signinManager = _signinManager;
            this.logger = logger;
        }
        public IActionResult EditUser(string Id)
        {
            if (Id is null) throw new ArgumentNullException();
            User? tempUser = user.GetByID(Id);
            return View(tempUser.ToView());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel euvm)
        {
            if (!ModelState.IsValid) return View(euvm);
            User? tempUser = euvm.FromView();
            if (!user.Edit(tempUser)) return View(euvm);
            if (!euvm.oldPassword.IsNullOrEmpty())
            {
                //User curUser = user.GetByID(euvm.NumberID);
                var curUser = await userManager.FindByIdAsync(euvm.NumberID);
                
                var check = await userManager.CheckPasswordAsync(curUser, euvm.oldPassword);
                if (!check)
                {
                    ModelState.AddModelError(string.Empty, "Старый пароль неверен");
                    return View(euvm);
                }

                var result=await userManager.ChangePasswordAsync(curUser,
                                        euvm.oldPassword, 
                                        euvm.password);
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(curUser);
                    await signinManager.RefreshSignInAsync(curUser);
                }
                    
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return View(euvm);
                }
            }
            await signinManager.RefreshSignInAsync(tempUser);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);
            var user = new User { UserName = Model.UserName, 
                                    Email=Model.Email, 
                                    user_Name=Model.user_Name
            };
            var regResult = await userManager.CreateAsync(user, Model.password);
            if (regResult.Succeeded)
            {
                logger.LogInformation("User {0} has registrated", user);

                await userManager.AddToRoleAsync(user, Role.Users); 

                await signinManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in regResult.Errors)
                ModelState.AddModelError("", error.Description);
            logger.LogWarning(string.Join(", ", regResult.Errors.Select(e=>e.Description)));
            return View(Model);

        }
        [AllowAnonymous]
        public IActionResult Login(string? _redirect) => View(new LoginUserViewModel {redirectUrl=_redirect });
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult AccessDenied(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

    }
}
