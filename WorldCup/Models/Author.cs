using System.ComponentModel.DataAnnotations;
using WorldCup.Data.Base;

namespace WorldCup.Models
{
	public class Author : IEntityBase
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
        [Display(Name="Name")]
		public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Surname")]
        public string Surname{ get; set; }
       

		public List<News> News { get; set; }
	}
}
