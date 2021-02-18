using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    public class ButonClickController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult onTapFindGame()
        {
            return Redirect("~/FindGame");
        }
        public IActionResult onTapCreateGame()
        {
            return Redirect("~/CreateGame");
        }
    }
}
