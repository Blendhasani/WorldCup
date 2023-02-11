using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WorldCup.Data.Services;
using WorldCup.Data.Static;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
	[Area("User")]
	public class StadiumsController : Controller
    {
        private readonly IStadiumsService _stadiumsService;
        public StadiumsController(IStadiumsService stadiumsService) 
        { 
            _stadiumsService = stadiumsService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? page)
        {
            var allStadiums = await _stadiumsService.GetAllAsync(page);
            return View(allStadiums);
        }
        //Get:
        public IActionResult Create()
        {
            return View();
        }

        //Get: Stadiums/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Capacity,Location,ImageURL")] Stadium stadium)
        {
            if (!ModelState.IsValid)
            {
                return View(stadium);
            }
            await _stadiumsService.AddAsync(stadium);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var stadiumsDetails = await _stadiumsService.GetByIdAsync(id);
            return View(stadiumsDetails);
        }

        //Get:
        public async Task<IActionResult> Edit(int id)
        {
            var stadiumsDetails = await _stadiumsService.GetByIdAsync(id);
            if (stadiumsDetails == null) return View("NotFound");
            return View(stadiumsDetails);
        }

        //Get: Cinemas/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity,Location,ImageURL")] Stadium stadium)
        {

            if (!ModelState.IsValid)
            {
                return View(stadium);
            }
            await _stadiumsService.UpdateAsync(id, stadium);
            return RedirectToAction(nameof(Index));
        }


        //Get:
        public async Task<IActionResult> Delete(int id)
        {
            var stadiumsDetails = await _stadiumsService.GetByIdAsync(id);
            if (stadiumsDetails == null) return View("NotFound");
            return View(stadiumsDetails);
        }

        //Get: Stadiums/Delete/1
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Deleteconfirmed(int id)
        {
            var stadiumDetails = await _stadiumsService.GetByIdAsync(id);
            if (stadiumDetails == null) return View("NotFound");

            await _stadiumsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
