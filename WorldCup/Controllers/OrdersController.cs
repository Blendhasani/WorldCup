using Microsoft.AspNetCore.Mvc;
using WorldCup.Data.Cart;
using WorldCup.Data.Services;
using WorldCup.Data.ViewModels;

namespace WorldCup.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IProductsService _productsService;
		private readonly ShoppingCart _shoppingCart;
		public OrdersController(IProductsService productsService, ShoppingCart shoppingCart)
		{
			_productsService = productsService;	
			_shoppingCart = shoppingCart;
		}
		public IActionResult Index()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;
			
			var response = new ShoppingCartVM()
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()

			};

			return View(response);
		}
	}
}
