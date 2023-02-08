using WorldCup.Models;

namespace WorldCup.Data.ViewModels
{
	public class ProductViewModel
	{
		public Product Product { get; set; }
		public IEnumerable<Product> Products { get; set; }
	}
}