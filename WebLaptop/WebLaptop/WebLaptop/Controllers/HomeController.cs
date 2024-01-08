using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebLaptop.Models;

namespace WebLaptop.Controllers
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
            
            return View();
        }
        public IActionResult RedirectToProductDetail(int id)
        {
            var redirectUrl = Url.Action("Index", "ProductDetail", new { productId = id });
            return Json(new { redirectUrl });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
