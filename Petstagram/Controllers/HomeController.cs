using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Petstagram.Models;
using Petstagram.Services;
using System.Diagnostics;

namespace Petstagram.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private HtmlSanitizerService _html;

        public HomeController(UserManager<User> user, SignInManager<User> signIn ,ILogger<HomeController> logger, HtmlSanitizerService html)
        {
            _html = html;
            _userManager = user;
            _signInManager = signIn;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Pet");
            } else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(FormRegister model)
        {
            if (ModelState.IsValid && _html.IsValid(model.Username) && _html.IsValid(model.Password))
            {
                var user = new User { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index");
                } else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        if(!_html.IsValid(model.Username))
                        {
                            ModelState.AddModelError("Username", "Username cannot contain <, >, &, ', or \"");
                        }

                        if(!_html.IsValid(model.Password))
                        {
                            ModelState.AddModelError("Password", "Password cannot contain <, >, &, ', or \"");
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new FormLogIn { ReturnUrl = returnUrl };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(FormLogIn model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                        model.Username,
                        model.Password,
                        isPersistent: model.RememberMe,
                        lockoutOnFailure: false
                    );
                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    } else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
