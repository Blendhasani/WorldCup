using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Data;
using WorldCup.Data.Services;
using WorldCup.Data.Static;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class HighlightsController : Controller
    {
        private readonly IHightlightsService _highlightsService;
        public HighlightsController(IHightlightsService highlightsService ) {
        _highlightsService= highlightsService;
        }
        [AllowAnonymous]
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
        public async Task<IActionResult> Create([Bind("Title,Description,ImgUrl,VideoUrl,Au")] Highlights Highlight)
        {

            if (!ModelState.IsValid)
            {
                return View(Highlight);
            }
            await _highlightsService.AddAsync(Highlight);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
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
