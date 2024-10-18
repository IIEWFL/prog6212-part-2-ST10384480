// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using ST10384480CMCS.Models;
using System.Diagnostics;

namespace ST10384480CMCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("SubmitClaim", "Claim");
        }

        public IActionResult Privacy()
        {
            return RedirectToAction("Privacy", "Claim");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}