using WorldCup.Data.Base;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
    public class HighlightsService:EntityBaseRepository<Highlights>,IHightlightsService
    {
        public HighlightsService(AppDbContext context) : base(context) { }
    }
}
