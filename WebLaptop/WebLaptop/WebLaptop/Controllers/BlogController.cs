using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
