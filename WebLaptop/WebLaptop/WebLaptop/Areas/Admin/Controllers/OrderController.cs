using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OrderController : Controller   
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetList()
        {
            return PartialView();
        }
    }
}
