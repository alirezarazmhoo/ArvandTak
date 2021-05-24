using Microsoft.AspNetCore.Mvc;

namespace AravandTak.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Detail()
		{
			return View();
		}
	}
}
