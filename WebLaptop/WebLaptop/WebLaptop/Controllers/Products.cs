using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Controllers
{
    public class Products : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RedirectToProductDetail(int id)
        {
            var redirectUrl = Url.Action("Index", "ProductDetail", new { productId = id });
            return Json(new { redirectUrl });
        }

    }
}
