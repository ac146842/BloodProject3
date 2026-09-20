using BloodProject3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BloodProject3.Controllers
{
    // Controller to handle main application pages like home, privacy policy, and error handling
    public class HomeController : Controller
    {
        // Displays the main landing page of the application
        public IActionResult Index()
        {
            return View();
        }

        // Diplays the privacy policy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Displays the default error page and passes request tracking info to prevent response caching
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Passes unique ID to the view for diagnostics
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
