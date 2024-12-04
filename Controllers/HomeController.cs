using System.Diagnostics;
using static FlowerShop_Website.Models.DBContext;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_Website.Models;

namespace FlowerShop_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        public HomeController(AppDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Flowers.ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult DangNhap()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
