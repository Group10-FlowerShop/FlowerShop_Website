using System.Diagnostics;
using static FlowerShop_Website.Models.DBContext;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_Website.Models;
using System.Text.Json;

namespace FlowerShop_Website.Controllers
{
    public class GioHangController : Controller
    {
        private readonly AppDbContext _context;
        public GioHangController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cart)
            ? new List<GioHang>()
            : JsonSerializer.Deserialize<List<GioHang>>(cart);

            return View(cartItems);
        }
        public IActionResult AddToCart(string productId, int quantity)
        {
            var cart = HttpContext.Session.GetString("Cart");
            List<GioHang> cartItems;
            
            if (string.IsNullOrEmpty(cart))
            {
                cartItems = new List<GioHang>();
            }
            else 
            {
                cartItems = JsonSerializer.Deserialize<List<GioHang>>(cart);
            }

            var product = _context.Flowers.Find(productId);
            var cartItem = new GioHang 
            {
                Id = product.flower_id,
                Name = product.name,
                Price = product.price,
                Quantity = quantity,
            };
            
            cartItems.Add(cartItem);

            // Lưu lại vào session
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
            
            return RedirectToAction("Index", "Home");
        }
    }
}
