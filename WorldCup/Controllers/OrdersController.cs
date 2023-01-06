﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorldCup.Data.Cart;
using WorldCup.Data.Services;
using WorldCup.Data.ViewModels;

namespace WorldCup.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private readonly IProductsService _productsService;
		private readonly ShoppingCart _shoppingCart;
		private readonly IOrdersService _ordersService;
		public OrdersController(IProductsService productsService, ShoppingCart shoppingCart, IOrdersService ordersService)
		{
			_productsService = productsService;	
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


		public async Task<IActionResult> CompleteOrder()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

			await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);

			await _shoppingCart.ClearShoppingCartAsync();

			return View("OrderCompleted");


		}
	}
}
