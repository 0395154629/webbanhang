using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebLaptop.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetList(int? id)
        {
            return PartialView();

        }
        public IActionResult Create()
        {
            return PartialView();
        }
        public IActionResult Edit(int id)
        {

            if (id != null)
            {
                ViewData["id"] = id;
                return PartialView();
            }
            else
            {
                return View("Index");
            }
        }
    }
}
