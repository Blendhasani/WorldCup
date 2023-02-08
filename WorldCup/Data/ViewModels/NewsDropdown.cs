using WorldCup.Areas.Admin.Models;

namespace WorldCup.Data.ViewModels
{
    public class NewsDropdown
    {
        public NewsDropdown() {
            Authors = new List<Author>();
        }
        public List<Author> Authors { get; set; }
    }
}
