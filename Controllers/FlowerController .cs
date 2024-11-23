using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using FlowerShop_Website.Data;
using FlowerShop_Website.Models;
using static FlowerShop_Website.Models.DBContext;
using System.IO;

namespace FlowerShop_Website.Controllers
{
    public class FlowerController : Controller
    {
        private readonly AppDbContext _context;

        public FlowerController(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var flowers = await _context.Flowers.ToListAsync();
        //    return View(flowers);
        //}

        public ActionResult Index(int page = 1, string sortOrder = "")
        {
            //sortOrder = string.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
            var flowers = _context.Flowers
        .Select(f => new Flower
        {
            flower_id = f.flower_id,
            name = f.name,
            description = f.description,
            price = f.price,
            status = f.status,
            MainImageUrl = "/images/" + f.FlowerImages.FirstOrDefault(img => img.image_type == "main").image_url
        });

            // Áp dụng sắp xếp
            switch (sortOrder)
            {
                case "price_asc":
                    flowers = flowers.OrderBy(f => f.price);
                    break;
                case "price_desc":
                    flowers = flowers.OrderByDescending(f => f.price);
                    break;
                case "mac_dinh":
                    flowers = flowers.OrderBy(f => f.flower_id);
                    break;
                default:
                   
                    break;
            }

            // Tính toán phân trang
            int pageSize = 9;
            var pagedList = flowers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Truyền dữ liệu vào ViewModel
            var model = new FlowerListViewModel
            {
                Flowers = pagedList,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(flowers.Count() / (double)pageSize),
                SortOrder = sortOrder // Để xác định lựa chọn ComboBox
            };

            return View(model);
        }

    }
}
