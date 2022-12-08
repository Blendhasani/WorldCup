using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WorldCup.Data.Services;
using WorldCup.Models;

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
        //get
        public IActionResult Create()
        {
            return View();
        }
        //Get :Highlights/Create

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Description,ImgUrl,VideoUrl")] Highlights Highlight)
        {

            if (!ModelState.IsValid)
            {
                return View(Highlight);
            }
            await _highlightsService.AddAsync(Highlight);
            return RedirectToAction(nameof(Index));
        }
    }
}
