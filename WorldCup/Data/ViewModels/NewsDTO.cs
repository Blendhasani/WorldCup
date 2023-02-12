using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WorldCup.Areas.Admin.Models;

namespace WorldCup.Data.ViewModels
{
	public class NewsDTO
	{

		
		public string Title { get; set; }
	
		public string Description { get; set; }


		public IFormFile ThumbnailUrl { get; set; }


		public IFormFile SecondaryImageUrl { get; set; }

		public string VideoUrl { get; set; }


		public DateTime CreatedDate { get; set; }

		public int AuthorId { get; set; }
	
		public Author Author { get; set; }

		public int ViewCount { get; set; }


	}
}
