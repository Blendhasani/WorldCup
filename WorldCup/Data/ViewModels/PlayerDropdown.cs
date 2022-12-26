using WorldCup.Areas.Admin.Models;
using WorldCup.Models;

namespace WorldCup.Data.ViewModels
{
    public class PlayerDropdown
    {
        public PlayerDropdown() {
            Clubs = new List<Club>();
        }
        public List<Club> Clubs { get; set; }
    }
}
