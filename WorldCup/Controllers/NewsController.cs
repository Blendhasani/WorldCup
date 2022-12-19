using Microsoft.AspNetCore.Mvc;
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
		public IActionResult Create()
		{
			return View();
		}
		//Get :News/Create

		[HttpPost]
		public async Task<IActionResult> Create([Bind("Title,Description,ThumbnailUrl,SecondaryImageUrl,VideoUrl,CreatedDate")] News news)
		{

			if (!ModelState.IsValid)
			{
				return View(news);
			}
			var newNews = new News()
			{
				Title = news.Title,
				Description = news.Description,
				ThumbnailUrl = news.ThumbnailUrl,
				SecondaryImageUrl = news.SecondaryImageUrl,
				VideoUrl = news.VideoUrl,
				CreatedDate = DateTime.Now,
			};
			await _newsService.AddAsync(newNews);
			return RedirectToAction(nameof(Index));
		}

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var newsDetails = await _newsService.GetByIdAsync(id);
            if (newsDetails == null) return View("NotFound");
            return View(newsDetails);
        }
        //News/Edit

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Title,Description,ThumbnailUrl,SecondaryImageUrl,VideoUrl,CreatedDate")] News news)
        {
            if (!ModelState.IsValid)
            {
                return View(news);
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
