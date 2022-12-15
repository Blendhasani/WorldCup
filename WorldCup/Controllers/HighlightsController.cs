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
        public async Task<IActionResult> Index(int? page)
        {
            var data = await _highlightsService.GetAllAsync(page);
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

        public async Task<IActionResult> Details(int id)
        {
            var highlightsDetails =await _highlightsService.GetByIdAsync(id);
            return View(highlightsDetails);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var highlightsDetails = await _highlightsService.GetByIdAsync(id);
            if (highlightsDetails == null) return View("Not Found");
            return View(highlightsDetails);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImgUrl,VideoUrl")] Highlights Highlight)
        {

            if (!ModelState.IsValid)
            {
                return View(Highlight);
            }
            await _highlightsService.UpdateAsync(id,Highlight);
            return RedirectToAction(nameof(Index));
        }

        //Highlights/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var highlightsDetails = await _highlightsService.GetByIdAsync(id);
            if (highlightsDetails == null) return View("NotFound");
            return View(highlightsDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Deleteconfirmed(int id)
        {
            var highlightsDetails = await _highlightsService.GetByIdAsync(id);
            if (highlightsDetails == null) return View("NotFound");

            await _highlightsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
