using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CreateGameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult onTapCanlce()
        {
            return Redirect("~/Home");
        }
        public IActionResult onTapCreate(string name, Tag[] tags)
        {
            return Redirect("~/Game");
        }
    }
}
