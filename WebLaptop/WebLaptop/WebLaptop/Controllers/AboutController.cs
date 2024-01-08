using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
