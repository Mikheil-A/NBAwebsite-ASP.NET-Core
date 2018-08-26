using Microsoft.AspNetCore.Mvc;
using NBAwebsite.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace NBAwebsite.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Title = "Register";
            return View();
        }

        [HttpPost]
        public IActionResult Register(SaveUserData model)
        {
            ViewBag.Title = "Register-POST";
            ViewBag.RegisterResult = model.insertIntoTBL();

            return View("Register");
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(CheckUserData md)
        {
            if (md.exists())
            {
                //Create sessions if an user logs in successfully
                HttpContext.Session.SetString("loggedInUsername", md.loggedInUsername);
                HttpContext.Session.SetString("loggedInEmail", md.loggedInUserEmail);
                HttpContext.Session.SetString("loggedInRegisterDate", md.loggedinUserRegisterDate);

                HttpContext.Session.SetString("LastRegisteredUsername", md.lastRegisteredUsename);
                HttpContext.Session.SetString("LastRegisteredUserDate", md.lastRegisteredDate);

                return RedirectToAction("Profile", "Account");
            }

            ViewData["Title"] = "Login - Account not found";
            ViewData["Message"] = "The email or the password you provided doesn't match any existing "
                + "accounts. Make sure you enter valid data or if you don't have an account register ";
            return View("Login");
        }

        public IActionResult Profile()
        {
            ViewBag.usern = HttpContext.Session.GetString("loggedInUsername");
            ViewBag.email = HttpContext.Session.GetString("loggedInEmail");
            ViewBag.date = HttpContext.Session.GetString("loggedInRegisterDate");

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}