using WorldCup.Areas.Admin.Models;

namespace WorldCup.Data.ViewModels
{
	public class ClubVM
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Birthday { get; set; }


		public IFormFile LogoUrl { get; set; }

		public string State { get; set; }


		public string City { get; set; }


		public List<Player> Players { get; set; }


	}
}
