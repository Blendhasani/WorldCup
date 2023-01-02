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

		public async Task<IActionResult> AddItemToShoppingCart(int id)
		{ 
			var item = await _productsService.GetByIdAsync(id);

			if (item != null)
			{ 
				_shoppingCart.AddItemToCart(item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}

		public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
		{
			var item = await _productsService.GetByIdAsync(id);

			if (item != null)
			{
				_shoppingCart.RemoveItemFromCart(item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}
	}
}
