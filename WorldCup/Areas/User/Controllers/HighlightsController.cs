using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WorldCup.Areas.Admin.Models;
using WorldCup.Data.Services;
using WorldCup.Data.Static;
using WorldCup.Data.ViewModels;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    [Area("User")]
    [Authorize(Roles = UserRoles.Admin)]
    public class HighlightsController : Controller
    {
        private readonly IHightlightsService _highlightsService;
        private readonly IPhotoService _photoService;

        public HighlightsController(IHightlightsService highlightsService, IPhotoService photoService)
        {
            _highlightsService = highlightsService;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(HighlightViewModel HighlightVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(HighlightVm.ImgUrl);
                var highlight = new Highlights
                {
                    Title = HighlightVm.Title,
                    Description = HighlightVm.Description,
                    ImgUrl = result.Url.ToString(),
                    VideoUrl = HighlightVm.VideoUrl,
                    Count = HighlightVm.Count,
                    CreationDate = HighlightVm.CreationDate
                };
                await _highlightsService.AddAsync(highlight);
                return RedirectToAction(nameof(Index));
            }
            else
            {

                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(HighlightVm);
     }

    
[AllowAnonymous]
public async Task<IActionResult> Details(int id)
{
    var highlightsDetails = await _highlightsService.GetByIdAsync(id);
    if (highlightsDetails == null)
        return View("Not Found");

    highlightsDetails.Count++;
    await _highlightsService.UpdateAsync(id, highlightsDetails);
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
    await _highlightsService.UpdateAsync(id, Highlight);
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
