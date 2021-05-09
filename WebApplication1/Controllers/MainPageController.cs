using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Pages;
using Microsoft.AspNetCore.Authorization;
namespace WebApplication1.Controllers
{
    public class MainPageController : Controller
    {
        public IActionResult _ViewStart()
        {
            return View();
        }
        [Authorize]
        public IActionResult _ViewStartAdmin()
        {
            return View();
        }
        public IActionResult _ViewLogin()
        {
            return View();
        }
    }
}
