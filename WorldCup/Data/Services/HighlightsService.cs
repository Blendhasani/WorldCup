using WorldCup.Data.Base;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
    public class HighlightsService:EntityBaseRepository<Highlights>,IHighlightsService
    {
        public HighlightsService(AppDbContext context) : base(context) { }
    }
}
