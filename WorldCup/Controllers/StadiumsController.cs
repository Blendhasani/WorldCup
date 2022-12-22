using Microsoft.AspNetCore.Mvc;
using WorldCup.Data.Services;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    public class StadiumsController : Controller
    {
        private readonly IStadiumsService _stadiumsService;
        public StadiumsController(IStadiumsService stadiumsService) 
        { 
            _stadiumsService = stadiumsService;
        }
        public async Task<IActionResult> Index()
        {
            var allStadiums = await _stadiumsService.GetAllAsync();
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
