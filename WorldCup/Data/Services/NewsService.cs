using WorldCup.Data.Base;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
    public class NewsService: EntityBaseRepository<News>, INewsService
    {
        public NewsService(AppDbContext context) : base(context) { }
    }
}
