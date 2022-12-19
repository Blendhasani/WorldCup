using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorldCup.Data.Services;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public async Task<IActionResult> Index(int? page)
        {
            var data = await _newsService.GetAllAsync(page);
            return View(data);
        }

		//get
		public async Task<IActionResult> Create()
		{
			var newsDropdownData = await _newsService.GetNewsDropdownValues();
			ViewBag.Authors = new SelectList(newsDropdownData.Authors, "Id", "Name"); 

            return View();
		}
		//Get :News/Create

		[HttpPost]
		public async Task<IActionResult> Create(News news)
		{

			if (!ModelState.IsValid)
			{
                var newsDropdownData = await _newsService.GetNewsDropdownValues();
                ViewBag.Authors = new SelectList(newsDropdownData.Authors, "Id", "Name");

                return View(news);
			}
		 await _newsService.AddNewNewsAsync(news);
			return RedirectToAction(nameof(Index));
		}

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
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
				AuthorId = newsDetails.AuthorId,
			};
			var newsDropdownData = await _newsService.GetNewsDropdownValues();
			ViewBag.Authors = new SelectList(newsDropdownData.Authors, "Id", "Name");
			return View(newsDetails);
        }
        //News/Edit

        [HttpPost]
        public async Task<IActionResult> Edit(int id, News news)
        {

			if(id !=news.Id)
			{
				if (!ModelState.IsValid)
				{
					var newsDropdownData = await _newsService.GetNewsDropdownValues();
					ViewBag.Authors = new SelectList(newsDropdownData.Authors, "Id", "Name");
					return View(news);
				}
			}
          
          
            await _newsService.UpdateAsync(id, news);
            return RedirectToAction(nameof(Index));
        }


		//Get: News/Delete
		public async Task<IActionResult> Delete(int id)
		{
			var newsDetails = await _newsService.GetByIdAsync(id);
			if (newsDetails == null) return View("NotFound");
			return View(newsDetails);
		}

		public async Task<IActionResult> Details(int id)
		{
			var newsDetails = await _newsService.GetByIdAsync(id);
			return View(newsDetails);
		}

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
