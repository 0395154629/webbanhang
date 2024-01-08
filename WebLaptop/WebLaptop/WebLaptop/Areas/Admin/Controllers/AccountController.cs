using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
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
