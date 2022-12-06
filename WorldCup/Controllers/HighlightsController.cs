using Microsoft.AspNetCore.Mvc;
using WorldCup.Data.Services;

namespace WorldCup.Controllers
{
    public class HighlightsController : Controller
    {
        private readonly IHighlightsService _highlightsService;
        public HighlightsController(IHighlightsService highlightsService ) {
        _highlightsService= highlightsService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _highlightsService.GetAllAsync();
            return View(data);
        }
    }
}
