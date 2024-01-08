using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
