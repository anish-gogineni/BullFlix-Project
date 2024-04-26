using AnishProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using AnishProject.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AnishProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var name = HttpContext.User.Identity;
                ViewBag.Name = name;
            }
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {

            var userData = _userRepository.GetUser(user);
            if (userData != null)
            {
                //if (userData.Role == "Admin")
                    //return RedirectToAction("Index", "Home");
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,userData.UserName),
                        new Claim(ClaimTypes.Role,userData.Role)
                    };
                var claimsidentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal userInfo = new ClaimsPrincipal(claimsidentity);
                if(userInfo.Identity.IsAuthenticated)
                    ViewData["IsAuthenticated"] = true;
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userInfo, new AuthenticationProperties()
                {
                    //IsPersistent = false,
                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(2),
                    AllowRefresh = true
                }
                );
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Login failed - handle the error
                TempData["LoginError"] = "Invalid username or password.";  // Store error message in TempData
                return RedirectToAction("Login", "Home");  // Redirect to Home with error message
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}