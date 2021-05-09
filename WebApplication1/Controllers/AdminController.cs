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
    public class AdminController : Controller
    {
        LibraryContext context;
        public AdminController(LibraryContext context)
        {
            this.context = context;
        }
        private void Auth(string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, userName) };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [Authorize]
        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult RegisterAdmin(AdminModel model)
        {
            if (ModelState.IsValid)
            {
                if (Check.CheckPasswordAndName(model))
                {
                    AdminModel admin = context.Admins.FirstOrDefault(a => a.Name == model.Name);
                    if (admin == null)
                    {
                        context.Admins.Add(new AdminModel { Name = model.Name, Password = model.Password });
                        context.SaveChanges();
                        return RedirectToAction("_ViewStartAdmin", "MainPage");
                    }
                }
                else
                {
                    return RedirectToAction("RegisterAdmin", "Admin");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginAdmin(AdminModel model)
        {
            if (ModelState.IsValid)
            {
                if (Check.CheckPasswordAndName(model))
                {
                    AdminModel admin = context.Admins.FirstOrDefault(a => a.Name == model.Name && a.Password == model.Password);
                    if (admin != null)
                    {
                        Auth(model.Name);
                        return RedirectToAction("_ViewStartAdmin", "MainPage");
                    }
                    else
                    {
                        return RedirectToAction("LoginAdmin", "Admin");
                    }
                }
            }
            return RedirectToAction("LoginAdmin", "Admin");
        }
        [Authorize]
        public IActionResult LogoutAdmin()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("_ViewLogin", "MainPage");
        }
    }
}
