using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class AuthorController : Controller
    {
        LibraryContext context;
        public AuthorController(LibraryContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewAuthor()//Admin
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult NewAuthor(AuthorModel model)//Admin
        {
            if (ModelState.IsValid && Check.CheckAuthor(model))
            {
                AuthorModel author = context.Authors.FirstOrDefault(a => a.FirstName == model.FirstName && a.LastName == model.LastName);
                if (author == null)
                {
                    context.Authors.Add(new AuthorModel { FirstName = model.FirstName, LastName = model.LastName });
                    context.SaveChanges();
                    return RedirectToAction("_ViewStartAdmin", "MainPage");
                }
                return RedirectToAction("NewAuthor", "Author");
            }
            return RedirectToAction("NewAuthor", "Author");
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditAuthor(int? id)
        {
            if(id == null)
                return RedirectToAction("AllAuthor", "Home");

            ViewBag.Id = id;
            
            AuthorModel author = context.Authors.Where(a => a.Id == id).First();

            return View(author);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditAuthor(AuthorModel author)
        {
            context.Authors.Update(author);
            context.SaveChanges();
            return RedirectToAction("AllAuthor", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult DeleteAuthor(int? id)
        {
            if (id == null)
                return RedirectToAction("AllAuthor", "Home");

            AuthorModel author = context.Authors.Where(a => a.Id == id).First();

            if(author ==null)
                return RedirectToAction("AllAuthor", "Home");

            context.Authors.Remove(author);
            context.SaveChanges();

            return RedirectToAction("AllAuthor", "Home");
        }
    }
}
