using CookieDeneme.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using CustomCookieBased.Configurations;
using CustomCookieBased.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CookieDeneme.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CookieContext _cookieContext;

        public HomeController(ILogger<HomeController> logger, CookieContext cookieContext)
        {
            _logger = logger;
            _cookieContext = cookieContext;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SignIn()
        {
            return View(new UserSignInModel());
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel userSignInModel)
        {

            var user = _cookieContext.Set<AppUser>().SingleOrDefault(x => x.Username == userSignInModel.Username.ToLower() && x.Password == userSignInModel.Password);

            if (user != null)
            {
                var roles = _cookieContext.Set<AppRole>().Where(x => x.UserRoles.Any(x => x.UserId == user.Id)).Select(x => x.Definition).ToList();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSignInModel.Username),

                };

                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = userSignInModel.RememberMe
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index");
            }

            return View(userSignInModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }

        public IActionResult AccesDenied()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}