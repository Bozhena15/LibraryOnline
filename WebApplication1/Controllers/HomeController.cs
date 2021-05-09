using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        LibraryContext context;
        public HomeController(LibraryContext context)
        {
            this.context = context;
        }
        public IActionResult AllBooks()//User
        {
            return View(context.Books.ToList());
        }
        public IActionResult AllAuthor()//Admin
        {
            return View(context.Authors.ToList());
        }
        public IActionResult AllGenre()//Admin
        {
            return View(context.GenreBooks.ToList());
        }
        public IActionResult AllBook()//Admin
        {
            return View(context.Books.ToList());
        }
        public IActionResult Contact()//User
        {
            return View();
        }

       
    }
}
