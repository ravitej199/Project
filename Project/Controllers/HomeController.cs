using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Diagnostics;
using System.Net;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult StatusCode(int code)
        {
            if (code == (int)HttpStatusCode.NotFound)
            {
                return View("NotFound");
            }

            return View("Error");
        }
    }
}
