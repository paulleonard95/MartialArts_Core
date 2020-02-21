using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Martial.Data.Services;
using Martial.Data.Models;
using Martial.Web.ViewModels;

namespace Martial.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly DataService _svc;

        public UserController()
        {
            _svc = new DataService();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] User m)
        {
            if (!ModelState.IsValid)
            {
                return View(m);
            }

            var user = _svc.Authenticate(m.Username, m.Password);
            if (user == null)
            {
                ModelState.AddModelError("Username", "Invalid Login Credentials");
                ModelState.AddModelError("Password", "Invalid Login Credentials");
                return View(m);
            }

            var principal = BuildPrincipal(user);
            // check user can login
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return Redirect("/");

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Username,Password,PasswordConfirm,Role")] RegisterViewModel m)
        {
            if (!ModelState.IsValid)
            {
                return View(m);
            }
            var user = _svc.RegisterUser(new User
            {
                Username = m.Username,
                Password = m.Password,
                Role = m.Role
            });

            // check user can login
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                BuildPrincipal(user)
            );
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }


        public IActionResult ErrorNotAuthorised() => View();
        public IActionResult ErrorNotAuthenticated() => View();


        // Claims can be added to customise the identity returned
        // new Claim("Id", u.Id), // user id added to identity 
        private ClaimsPrincipal BuildPrincipal(User u)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, u.Username),
                new Claim(ClaimTypes.Role, u.Role.ToString())
            }, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}