using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sistema_registro_documentacion.Models;
using Sistema_registro_documentacion.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly IGenericRepository<Login> _loginRepo;
        public LoginController(IGenericRepository<Login> loginRepo)
        {
            _loginRepo = loginRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            Login login = new Login();
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                List<Login>  _user = new List<Login>();
                List<string> parameter = new List<string>();
                parameter.Add(login.usuario);
                parameter.Add(login.password);
                _user = _loginRepo.Filter(parameter);
                if (_user.Count != 0)
                {
                    var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,_user[0].usuario),
                    new Claim(ClaimTypes.Role,_user[0].rol)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Revise que los campos sean correctos");
                    return View();
                }
            }
            return View(login);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
    }
}
