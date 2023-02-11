using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using WorldCup.Data;
using WorldCup.Data.Cart;
using WorldCup.Data.Enums;
using WorldCup.Data.Services;
using WorldCup.Data.ViewModels;
using WorldCup.Models;

namespace WorldCup.Controllers
{

	[Authorize]
	[Area("User")]
	public class OrdersController : Controller
	{
		private readonly IProductsService _productsService;
        private readonly AppDbContext _context;
        private readonly ShoppingCart _shoppingCart;
		private readonly IOrdersService _ordersService;
		public OrdersController(IProductsService productsService, AppDbContext context,ShoppingCart shoppingCart, IOrdersService ordersService)
		{
			_productsService = productsService;
            _context = context;
            _shoppingCart = shoppingCart;
			_ordersService = ordersService;
		}
		public async Task<IActionResult> Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string userRole = User.FindFirstValue(ClaimTypes.Role);
			
			var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
			return View(orders);
		}
		public IActionResult ShoppingCart()
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


        //public async Task<IActionResult> CompleteOrder()
        //{
        //	var items = _shoppingCart.GetShoppingCartItems();
        //	string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //	string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
        //	string userRole = User.FindFirstValue(ClaimTypes.Role);

        //	await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
        //	//qekjo posht e merr order nbaz te qasej user id) 
        //	var orderi = _context.Orders.FirstOrDefault(x => x.UserId == userId);

        //	await _context.SaveChangesAsync();
        //	await _shoppingCart.ClearShoppingCartAsync();

        //	return View("OrderCompleted");


        //}
        public DateTime CalculateShipmentDate(Order order)
        {
            if (order.OrderItems.Any(i => i.Product.ProductType == ProductType.Jersey))
            {
                return order.CreatedAt.AddDays(7);
            }
            else
            {
                return order.CreatedAt.AddDays(10);
            }
        }

        public async Task<Order> StoreOrderAsync(IEnumerable<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order
            {
                UserId = userId,
                Email = userEmailAddress,
                OrderItems = items.Select(i => new OrderItem
                {
                    ProductId = i.Product.Id,
                    Amount = i.Amount
                }).ToList()
            };

            order.ShipmentDate = CalculateShipmentDate(order);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }



    }
}
