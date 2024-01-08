using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
