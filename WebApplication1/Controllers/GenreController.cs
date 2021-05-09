using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class GenreController : Controller
    {
        LibraryContext context;
        public GenreController(LibraryContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewGenre()//Admin
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewGenre(GenreBookModel model)//Admin
        {
            if (ModelState.IsValid && Check.CheckGenre(model))
            {
                GenreBookModel genreBook = context.GenreBooks.FirstOrDefault(g => g.Genre == model.Genre);
                if (genreBook == null)
                {
                    context.GenreBooks.Add(new GenreBookModel { Genre = model.Genre });
                    context.SaveChanges();
                    return RedirectToAction("_ViewStartAdmin", "MainPage");
                }
                return RedirectToAction("NewGenre", "Genre");
            }
            return RedirectToAction("NewGenre", "Genre");
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditGenre(int? id)
        { 
            if (id == null)
                return RedirectToAction("AllGenre", "Home");

            ViewBag.Id = id;
            GenreBookModel genre = context.GenreBooks.Where(a => a.Id == id).First();

            return View(genre);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditGenre(GenreBookModel genreBook)
        {
            context.GenreBooks.Update(genreBook);
            context.SaveChanges();
            return RedirectToAction("AllGenre", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult DeleteGenre(int? id)
        {
            if (id == null)
                return RedirectToAction("AllGenre", "Home");

            GenreBookModel genre = context.GenreBooks.Where(a => a.Id == id).First();

            if (genre == null)
                return RedirectToAction("AllGenre", "Home");

            context.GenreBooks.Remove(genre);
            context.SaveChanges();

            return RedirectToAction("AllGenre", "Home");
        }
    }
}
