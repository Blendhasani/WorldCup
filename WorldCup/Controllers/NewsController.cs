using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WorldCup.Data;
using WorldCup.Data.Services;
using WorldCup.Data.Static;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        public static int v = 0;
   
        public NewsController(INewsService newsService, UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _newsService = newsService;
            _userManager = userManager;
            _context = context;

   
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
		public async Task<IActionResult> Create(News news)
		{
            int vr = v+1;
            var user = await GetCurrentUserAsync();
            var namee = user.FullName;
            var authorr = _context.Authors.Where(s=>s.Name.Equals(namee)).FirstOrDefault();
            if (!ModelState.IsValid)
			{
                var newsDropdownData = await _newsService.GetNewsDropdownValues();
                ViewBag.Authors = new SelectList(newsDropdownData.Authors, "Id", "Name");

                return View(news);
			}
            var n = new News()
            {
                Id = news.Id,
                ThumbnailUrl = news.ThumbnailUrl,
                Description = news.Description,
                VideoUrl = news.VideoUrl,
                Title = news.Title,
                SecondaryImageUrl = news.SecondaryImageUrl,
                CreatedDate = DateTime.Now,
                AuthorId = authorr.Id,
                ViewCount = vr,
            };
        
            await _newsService.AddNewNewsAsync(n);
          
          
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
                ViewCount= v,
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
                ViewCount = v,
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

        public async Task<IActionResult> Details(int id,News news)
		{
            //var newsDetails = await _newsService.GetNewsByIdAsync(id);
            var newsDetails = await _context.News.Include(s=>s.Author).FirstOrDefaultAsync();
            if (newsDetails == null) return View("NotFound");

           
/*   
            var response = new News()
            {
                Id= news.Id,
                Title = newsDetails.Title,
                Description = newsDetails.Description,
                ThumbnailUrl = newsDetails.ThumbnailUrl,
                SecondaryImageUrl = newsDetails.SecondaryImageUrl,
                VideoUrl = newsDetails.VideoUrl,
                CreatedDate = DateTime.Now,
                AuthorId = news.AuthorId,
                ViewCount = news.ViewCount++,
            };
            await _newsService.UpdateAsync(id, response);*/
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
