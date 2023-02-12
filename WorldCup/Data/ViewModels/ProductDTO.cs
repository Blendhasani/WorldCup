using WorldCup.Data.Base;
using WorldCup.Data.Enums;

namespace WorldCup.Data.ViewModels
{
	public class ProductDTO
	{ 
			public string Name { get; set; }

			public string Description { get; set; }	
			public IFormFile ImgUrl { get; set; }

			public double Price { get; set; }

			public ProductType ProductType { get; set; }

			public State State { get; set; }
		
	}
}
