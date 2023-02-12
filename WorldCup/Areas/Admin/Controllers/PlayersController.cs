using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using WorldCup.Areas.Admin.Models;
using WorldCup.Data;
using WorldCup.Data.Services;
using WorldCup.Data.Static;

namespace WorldCup.Areas.Admin.Controllers
{
   

    [Area("Admin")]
   
    public class PlayersController : Controller
    {
        private readonly IPlayersService _service;

        public PlayersController(IPlayersService service)
        {
            _service = service;
        }
        
        [Route("Players/Index")]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Players/Create
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create()
        {
            var playerDropdownData = await _service.GetPlayerDropdownValues();
            ViewBag.Clubs = new SelectList(playerDropdownData.Clubs, "Id", "Name");
            return View();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(Player player)
        {
            if (!ModelState.IsValid)
            {
                var playerDropdownData = await _service.GetPlayerDropdownValues();
                ViewBag.Clubs = new SelectList(playerDropdownData.Clubs, "Id", "Name");
                return View(player);
            }
            await _service.AddAsync(player);
            return RedirectToAction(nameof(Index));
        }
   
        //Get: Actors/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var playerDetails = await _service.GetPlayerByIdAsync(id);

            if (playerDetails == null) return View("NotFound");
            return View(playerDetails);
        }

        [Authorize(Roles = UserRoles.Admin)]
        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var playerDetails = await _service.GetByIdAsync(id);
            if (playerDetails == null) return View("NotFound");
            var response = new Player()
            {
                FullName = playerDetails.FullName,
                Surname = playerDetails.Surname,
                Bio = playerDetails.Bio,
                Birthday = playerDetails.Birthday,
                ClubId = playerDetails.ClubId,
                State = playerDetails.State,
            };
            var playerDropdownData = await _service.GetPlayerDropdownValues();
            ViewBag.Clubs = new SelectList(playerDropdownData.Clubs, "Id", "Name");
            return View(playerDetails);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Player player)
        {

            if (id != player.Id)
            {
                if (!ModelState.IsValid)
                {
                    var playerDropdownData = await _service.GetPlayerDropdownValues();
                    ViewBag.Clubs = new SelectList(playerDropdownData.Clubs, "Id", "Name");
                    return View(player);
                }
            }


            await _service.UpdateAsync(id, player);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = UserRoles.Admin)]
        //Get: Players/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var playerDetails = await _service.GetByIdAsync(id);
            if (playerDetails == null) return View("NotFound");
            return View(playerDetails);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerDetails = await _service.GetByIdAsync(id);
            if (playerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
