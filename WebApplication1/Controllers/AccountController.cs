using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        LibraryContext context;
        public AccountController(LibraryContext context)
        {
            this.context = context;
        }
        private void Auth(string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, userName) };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (Check.CheckPasswordAndEmail(model))
                {
                    UserModel user = context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                    if (user != null)
                    {
                        Auth(model.Email);
                        return RedirectToAction("_ViewStart", "MainPage");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            return View();
        }
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Register()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            if(ModelState.IsValid)
            {
                if (Check.CheckRegister(model))
                {
                    UserModel user = context.Users.FirstOrDefault(u => u.Email == model.Email && u.Phone == model.Phone);
                    if (user == null)
                    {
                        context.Users.Add(new UserModel { Name = model.Name, Phone = model.Phone, Password = model.Password, Email = model.Email });
                        context.SaveChanges();
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Register", "Account");
                }
            }
            return View(model);
        }

    }
}
