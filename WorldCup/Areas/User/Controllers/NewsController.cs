﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WorldCup.Data;
using WorldCup.Data.Services;
using WorldCup.Data.Static;
using WorldCup.Data.ViewModels;
using WorldCup.Models;

namespace WorldCup.Controllers
{
	[Area("User")]
	[Authorize]
	public class NewsController : Controller
	{
		private readonly INewsService _newsService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly AppDbContext _context;
		private readonly IPhotoService _photoService;
		public static int v = 0;

		public NewsController(INewsService newsService, UserManager<ApplicationUser> userManager, AppDbContext context, IPhotoService photoService)
		{
			_newsService = newsService;
			_userManager = userManager;
			_context = context;
			_photoService = photoService;

		}

		private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
		[AllowAnonymous]
		public async Task<IActionResult> Index(int? page)
		{
			var data = await _newsService.GetAllAsync(page);
			return View(data);
		}

		//get
		[Authorize(Roles = "Editor")]
		public async Task<IActionResult> Create()
		{
			var newsDropdownData = await _newsService.GetNewsDropdownValues();
			ViewBag.Authors = new SelectList(newsDropdownData.Authors, "Id", "Name");

			return View();
		}
		//Get :News/Create
		[Authorize(Roles = "Editor")]
		[HttpPost]
		public async Task<IActionResult> Create(NewsDTO newsDto)
		{
			if (!ModelState.IsValid)
			{
				var newsDropdownData = await _newsService.GetNewsDropdownValues();
				ViewBag.Authors = new SelectList(newsDropdownData.Authors, "Id", "Name");

				return View(newsDto);
			}
			var result = await _photoService.AddPhotoAsync(newsDto.ThumbnailUrl);
			var result2 = await _photoService.AddPhotoAsync(newsDto.SecondaryImageUrl);
			var user = await GetCurrentUserAsync();
			var namee = user.FullName;
			var authorr = _context.Authors.Where(s => s.Name.Equals(namee)).FirstOrDefault();

			var news = new News
			{
				Title = newsDto.Title,
				Description = newsDto.Description,
				ThumbnailUrl = result.Url.ToString(),
				SecondaryImageUrl = result2.Url.ToString(),
				VideoUrl = newsDto.VideoUrl,
				CreatedDate = newsDto.CreatedDate,
				AuthorId = authorr.Id,
				ViewCount = newsDto.ViewCount
			};

			await _newsService.AddAsync(news);
			return RedirectToAction(nameof(Index));
		}
		// Edit
		[Authorize(Roles = "Editor")]
		public async Task<IActionResult> Edit(int id)
		{
			var user = await GetCurrentUserAsync();
			var namee = user.FullName;
			var authorr = _context.Authors.Where(s => s.Name.Equals(namee)).FirstOrDefault();
			var newsDetails = await _newsService.GetByIdAsync(id);
			if (newsDetails == null) return View("NotFound");
			var response = new News()
			{
				Title = newsDetails.Title,
				Description = newsDetails.Description,
				ThumbnailUrl = newsDetails.ThumbnailUrl,
				SecondaryImageUrl = newsDetails.SecondaryImageUrl,
				VideoUrl = newsDetails.VideoUrl,
				CreatedDate = DateTime.Now,
				AuthorId = authorr.Id,
				ViewCount = newsDetails.ViewCount,
			};

			var newsDropdownData = await _newsService.GetNewsDropdownValues();
			ViewBag.Authors = new SelectList(newsDropdownData.Authors, "Id", "Name");
			return View(newsDetails);
		}
		//News/Edit
		[Authorize(Roles = "Editor")]
		[HttpPost]
		public async Task<IActionResult> Edit(int id, News news)
		{

			/*			if(id !=news.Id)
                        {
                            if (!ModelState.IsValid)
                            {
                                var newsDropdownData = await _newsService.GetNewsDropdownValues();
                                ViewBag.Authors = new SelectList(newsDropdownData.Authors, "Id", "Name");
                                return View(news);
                            }
                        }*/

			var newsDetails = await _newsService.GetByIdAsync(id);
			if (newsDetails == null) return View("NotFound");
			var response = new News()
			{
				Title = newsDetails.Title,
				Description = newsDetails.Description,
				ThumbnailUrl = newsDetails.ThumbnailUrl,
				SecondaryImageUrl = newsDetails.SecondaryImageUrl,
				VideoUrl = newsDetails.VideoUrl,
				CreatedDate = DateTime.Now,
				AuthorId = news.AuthorId,
				ViewCount = newsDetails.ViewCount,
			};

			await _newsService.UpdateAsync(id, response);
			return RedirectToAction(nameof(Index));
		}


		//Get: News/Delete
		[Authorize(Roles = "Editor")]
		public async Task<IActionResult> Delete(int id)
		{
			var newsDetails = await _newsService.GetByIdAsync(id);
			if (newsDetails == null) return View("NotFound");
			return View(newsDetails);
		}
		[AllowAnonymous]


		public async Task<IActionResult> Details(int id)
		{


			var newsDetails = await _newsService.GetNewsByIdAsync(id);
			newsDetails.ViewCount += 1;

			/*var newsU = new News()
			{
				Id = newsDetails.Id,
				Title = newsDetails.Title,
				Description = newsDetails.Description,
				ThumbnailUrl = newsDetails.ThumbnailUrl,
				SecondaryImageUrl = newsDetails.SecondaryImageUrl,
				VideoUrl = newsDetails.VideoUrl,
				CreatedDate = DateTime.Now,
				AuthorId = newsDetails.AuthorId,
				ViewCount = newsDetails.ViewCount++,
			};*/
			await _newsService.UpdateAsync(id, newsDetails);

			return View(newsDetails);
		}

		[Authorize(Roles = "Editor")]
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var newsDetails = await _newsService.GetByIdAsync(id);
			if (newsDetails == null) return View("NotFound");


			await _newsService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
