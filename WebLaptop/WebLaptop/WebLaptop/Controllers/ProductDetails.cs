using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Controllers
{
    public class ProductDetail : Controller
    {
        public IActionResult Index(int productId)
        {
            var id = productId;
            ViewData["id"] = id;
            return View("Index");
        }
    }
}
