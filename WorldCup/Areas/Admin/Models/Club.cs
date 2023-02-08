using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WorldCup.Data.Base;

namespace WorldCup.Areas.Admin.Models
{
    public class Club :IEntityBase
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[Display(Name = "Name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Birthday is required")]
		[Display(Name = "Birthday")]
		public string Birthday { get; set; }

		[Required(ErrorMessage = "Logo is required")]
		[Display(Name = "Logo")]
		public string LogoUrl { get; set; }

		[Required(ErrorMessage = "State is required")]
		[Display(Name = "State")]
		public string State { get; set; }

		[Required(ErrorMessage = "City is required")]
		[Display(Name = "City")]
		public string City { get; set; }


        public List<Player> Players { get; set; }


    }
}
