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

        [Route("Flower/Detail/{flowerId}")]
        public async Task<ActionResult> Detail(string flowerId)
        {
            // Lấy thông tin chi tiết của hoa
            var flower = await _context.Flowers
                .Include(f => f.FlowerImages) // Bao gồm các hình ảnh
                .Where(f => f.flower_id == flowerId)
                .FirstOrDefaultAsync();

            if (flower == null)
            {
                return NotFound();
            }

            // Tính toán URL của hình ảnh chính
            flower.MainImageUrl = flower.FlowerImages
                .FirstOrDefault(img => img.image_type == "main")?.image_url ?? "/images/default.jpg";

            // Lấy danh mục liên quan
            var categories = await _context.FlowerCategories
                .Where(fc => fc.flower_id == flowerId)
                .Select(fc => fc.Category)
                .ToListAsync();

            // Lấy các dịp liên quan
            var occasions = await _context.FlowerOccasions
                .Where(fo => fo.flower_id == flowerId)
                .Select(fo => fo.Occasion)
                .ToListAsync();

            // Lấy các màu sắc liên quan
            var colors = await _context.FlowerColors
                .Where(fc => fc.flower_id == flowerId)
                .Select(fc => fc.Color)
                .ToListAsync();

            // Lấy các phong cách liên quan
            var styles = await _context.FlowerStyles
                .Where(fs => fs.flower_id == flowerId)
                .Select(fs => fs.Style)
                .ToListAsync();

            // Tạo ViewModel với đầy đủ thông tin
            var model = new FlowerDetailViewModel
            {
                Flower = flower,
                Categories = categories,
                Occasions = occasions,
                Colors = colors,
                Styles = styles,
                Images = flower.FlowerImages.Select(img => $"/images/{img.image_url}").ToList()
            };
            return View(model);
        }

    }
}
