using WorldCup.Data.Base;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
	public class ProductsService : EntityBaseRepository<Product> ,IProductsService
	{

		public ProductsService(AppDbContext context) : base(context) { }

	
	}
}
