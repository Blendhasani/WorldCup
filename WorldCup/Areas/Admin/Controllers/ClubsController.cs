using Microsoft.AspNetCore.Mvc;

namespace WorldCup.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ClubsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
