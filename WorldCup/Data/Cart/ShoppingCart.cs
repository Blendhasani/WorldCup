using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using System.ServiceModel.Channels;
using WorldCup.Models;
using ISession = Microsoft.AspNetCore.Http.ISession;

namespace WorldCup.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;
        private static object cartld;

        public ShoppingCart(AppDbContext context)
        {
            _context= context;  
        }

        public void AddItemToCart(Product product)
        {

            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n=>n.Product.Id == product.Id && n.ShoppingCartId==ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1,
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider services)

        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };

        }

      

        public void RemoveItemFromCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.Id == product.Id && n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }

                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            
            _context.SaveChanges();
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public object ShoppingCartld { get; private set; }

        //method to get all shopping card items
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId ==
            ShoppingCartId).Include(n => n.Product).ToList());

        }
        //get total of shopping card items price
        public double GetShoppingCartTotal()
        {
            //take the prices of the products in the list of shoppingcart
            // add them up/
            //show the result

            var total = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Product.Price * n.Amount).Sum();
            return total;
        }

    }
}
