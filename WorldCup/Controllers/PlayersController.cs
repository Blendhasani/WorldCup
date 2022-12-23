﻿using Microsoft.AspNetCore.Mvc;
using WorldCup.Data;
using WorldCup.Data.Services;
using WorldCup.Models;

namespace WorldCup.Controllers
{
	public class PlayersController : Controller
	{
		private readonly IPlayersService _service;

		public PlayersController(IPlayersService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var data = await _service.GetAllAsync();
			return View(data);
		}

		//Get: Players/Create
		public IActionResult Create()
		{ 
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Player player)
		{ 
			if(!ModelState.IsValid)
			{
				return View(player);
			}
			await _service.AddAsync(player);
			return RedirectToAction(nameof(Index));
		}

		//Get: Actors/Details/1
		public async Task<IActionResult> Details(int id)
		{ 
			var playerDetails = await _service.GetByIdAsync(id);

			if (playerDetails == null) return View("NotFound");
			return View(playerDetails);
		}

		//Get: Players/Edit/1
		public async Task <IActionResult> Edit(int id)
		{
			var playerDetails = await _service.GetByIdAsync(id);
			if (playerDetails == null) return View("NotFound");
			return View(playerDetails);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Player player)
		{
			if (!ModelState.IsValid)
			{
				return View(player);
			}
			await _service.UpdateAsync(id, player);
			return RedirectToAction(nameof(Index));
		}

		//Get: Players/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var playerDetails = await _service.GetByIdAsync(id);
			if (playerDetails == null) return View("NotFound");
			return View(playerDetails);
		}

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