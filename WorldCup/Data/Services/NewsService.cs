using Microsoft.EntityFrameworkCore;
using WorldCup.Data.Base;
using WorldCup.Data.ViewModels;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
    public class NewsService : EntityBaseRepository<News>, INewsService
    {
        public NewsService(AppDbContext context) : base(context) { }

		public async Task AddNewNewsAsync(News news)
		{
			var newNews = new News()
			{
				Title = news.Title,
				Description = news.Description,
				ThumbnailUrl = news.ThumbnailUrl,
				SecondaryImageUrl = news.SecondaryImageUrl,
				VideoUrl = news.VideoUrl,
				CreatedDate = DateTime.Now,
				AuthorId = news.AuthorId,
				ViewCount= news.ViewCount,
			};
			await _context.News.AddAsync(newNews);
			await _context.SaveChangesAsync();

		}

		public async Task<NewsDropdown> GetNewsDropdownValues()
        {
            var response = new NewsDropdown()
            {
                Authors = await _context.Authors.OrderBy(n => n.Name).ToListAsync()
        };
         return response;
        }

		public async Task<News> GetNewsByIdAsync(int id)
		{
			var newsDetails = await _context.News
				.Include(c => c.Author)
				.FirstOrDefaultAsync(n => n.Id == id);

			return newsDetails;
		}
	}
         
           
        }

