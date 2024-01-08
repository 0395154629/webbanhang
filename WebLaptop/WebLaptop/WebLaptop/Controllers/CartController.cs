using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
