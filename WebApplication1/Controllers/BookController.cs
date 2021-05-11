using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
        LibraryContext context;
        public BookController(LibraryContext context)
        {
            this.context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult NewBook()
        {
            ViewBag.GenreBook = new SelectList(context.GenreBooks.ToList(), "Id", "Genre");
            ViewBag.AuthorL = new SelectList(context.Authors.ToList(), "Id", "LastName");
            ViewBag.AuthorF = new SelectList(context.Authors.ToList(), "Id", "FirstName");

            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult NewBook(BookModel model)
        {
            if (ModelState.IsValid && Check.CheckBook(model))
            {
                int genreId = Convert.ToInt32(Request.Form.FirstOrDefault(g => g.Key == "GenreBook").Value);
                GenreBookModel genre = context.GenreBooks.FirstOrDefault(g => g.Id == genreId);

                int authorId = Convert.ToInt32(Request.Form.FirstOrDefault(l => l.Key == "AuthorL").Value);
                AuthorModel author = context.Authors.FirstOrDefault(a => a.Id == authorId);

                BookModel book = context.Books.FirstOrDefault(b => b.Name == model.Name);
                if (book == null)
                {
                    context.Books.Add(new BookModel { Name = model.Name, Link = model.Link, YearOfPublic = model.YearOfPublic, GenreBook = genre, Author = author });
                    context.SaveChanges();
                    return RedirectToAction("_ViewStartAdmin", "MainPage");
                }
                return RedirectToAction("NewBook", "Book");
            }
            return RedirectToAction("NewBook", "Book");
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditBook(int? id)
        {
            if (id == null)
                return RedirectToAction("AllBook", "Home");

            ViewBag.Id = id;
            ViewBag.GenreBook = new SelectList(context.GenreBooks.ToList(), "Id", "Genre");
            ViewBag.AuthorL = new SelectList(context.Authors.ToList(), "Id", "LastName");
            ViewBag.AuthorF = new SelectList(context.Authors.ToList(), "Id", "FirstName");
            BookModel book = context.Books.Where(a => a.Id == id).First();

            return View(book);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditBook(BookModel book)
        {
            int genreId = Convert.ToInt32(Request.Form.FirstOrDefault(g => g.Key == "GenreBook").Value);
            GenreBookModel genre = context.GenreBooks.FirstOrDefault(g => g.Id == genreId);

            int authorId = Convert.ToInt32(Request.Form.FirstOrDefault(l => l.Key == "AuthorL").Value);
            AuthorModel author = context.Authors.FirstOrDefault(a => a.Id == authorId);

            book.GenreBook = genre;
            book.Author = author;

            context.Books.Update(book);
            context.SaveChanges();
            return RedirectToAction("AllBook", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult DeleteBook(int? id)
        {
            if (id == null)
                return RedirectToAction("AllBook", "Home");

            BookModel book = context.Books.Where(a => a.Id == id).First();

            if (book == null)
                return RedirectToAction("AllBook", "Home");

            context.Books.Remove(book);
            context.SaveChanges();

            return RedirectToAction("AllBook", "Home");
        }
    }
}
