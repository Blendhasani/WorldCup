using System.ComponentModel.DataAnnotations;
using WorldCup.Data.Base;
using WorldCup.Data.Enums;

namespace WorldCup.Models
{
	public class Product:IEntityBase
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }	
		public string ImgUrl { get; set; }

		public double Price { get; set; }

		public ProductType ProductType { get; set; }

		public State State { get; set; }
	}
}
