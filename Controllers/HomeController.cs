using Test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult AddProduct()
        {
            return View("AddProduct");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
