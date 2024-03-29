﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
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
        public OrdersController(IProductsService productsService, AppDbContext context, ShoppingCart shoppingCart, IOrdersService ordersService)
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


        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            var order = _context.Orders.FirstOrDefault(x => x.UserId == userId);
            order.ShipmentDate = DateTime.Now.AddDays(10);

            await _context.SaveChangesAsync();
            await _shoppingCart.ClearShoppingCartAsync();

            // Send email to user with details of the order
            try
            {

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("mygganbu@gmail.com");
                    mail.To.Add(userEmailAddress);
                    mail.Subject = "Order Confirmation";
                    string emailBody = "Dear Customer,\n\n";
                    emailBody += "Thank you for placing an order with us. Your order details are as follows:\n\n";
                    emailBody += "Order Number: " + order.Id + "\n";
                   
                    emailBody += "Estimated Shipment Date: " + order.ShipmentDate + "\n\n";
                    emailBody += "Items Purchased:\n";
                    emailBody += "\nThank you for your business!\n\nBest regards,\nMy Online Store";
                    mail.Body = emailBody;
                    mail.IsBodyHtml = false;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("saraqialbin@gmail.com", "feiippdqwspvojia");
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                }
            }
            catch
            {
                // Log error message or display error message to user
            }

            return View("OrderCompleted");


        }

    }
}
