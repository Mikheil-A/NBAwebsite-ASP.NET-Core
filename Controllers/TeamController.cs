using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NBAwebsite.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult LosAngelesLakers()
        {
            ViewBag.Title = "Los Angeles Lakers";
            return View();
        }

        public IActionResult MiamiHeat()
        {
            ViewBag.Title = "Miami Heat";
            return View();
        }

        public IActionResult GoldenStateWarriors()
        {
            ViewBag.Title = "Golden State Warriors";
            return View();
        }

        public IActionResult SanAntonioSpurs()
        {
            ViewBag.Title = "San Antonio Spurs";
            return View();
        }
    }
}