using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace FlowerShop_Website.Models
{
    [Table("flowers")]
    public class Flower
    {
        [Key]
        public string flower_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string status { get; set; }

        // Mối quan hệ với FlowerImage
        public ICollection<FlowerImage> FlowerImages { get; set; }

        // Mối quan hệ với FlowerCategory (qua bảng FlowerCategory)
        public ICollection<FlowerCategory> FlowerCategories { get; set; }

        // Mối quan hệ với FlowerOccasion (qua bảng FlowerOccasion)
        public ICollection<FlowerOccasion> FlowerOccasions { get; set; }

        // Mối quan hệ với FlowerColor (qua bảng FlowerColor)
        public ICollection<FlowerColor> FlowerColors { get; set; }

        // Mối quan hệ với FlowerStyle (qua bảng FlowerStyle)
        public ICollection<FlowerStyle> FlowerStyles { get; set; }

        [NotMapped]
        public string MainImageUrl { get; set; }
    }

    [Table("flower_images")]
    public class FlowerImage
    {
        [Key]
        public string image_id { get; set; }  // Khoá chính

        public string flower_id { get; set; }  // Khoá ngoại trỏ tới bảng Flower
        public string image_url { get; set; }
        public string image_type { get; set; }
        public int display_order { get; set; }

        // Mối quan hệ với bảng Flower
        public Flower Flower { get; set; }
    }

    public class FlowerListViewModel
    {
        public IEnumerable<Flower> Flowers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SortOrder { get; set; }
    }
    public class FlowerDetailViewModel
    {
        public Flower Flower { get; set; }
        public List<Category> Categories { get; set; }
        public List<Occasion> Occasions { get; set; }
        public List<ColorOfFlower> Colors { get; set; }
        public List<Style> Styles { get; set; }
        // Danh sách URL hình ảnh
        public List<string> Images { get; set; }
    }

    public class FlowerCategory
    {
        [ForeignKey("Flower")]
        public string flower_id { get; set; }

        [ForeignKey("Category")]
        public string category_id { get; set; }

        public Flower Flower { get; set; }
        public Category Category { get; set; }
    }

    public class FlowerOccasion
    {
        [ForeignKey("Flower")]
        public string flower_id { get; set; }

        [ForeignKey("Occasion")]
        public string occasion_id { get; set; }

        public Flower Flower { get; set; }
        public Occasion Occasion { get; set; }
    }

    public class FlowerColor
    {
        [ForeignKey("Flower")]
        public string flower_id { get; set; }

        [ForeignKey("ColorOfFlower")]
        public string color_id { get; set; }

        public Flower Flower { get; set; }
        public ColorOfFlower Color { get; set; }
    }

    public class FlowerStyle
    {
        [ForeignKey("Flower")]
        public string flower_id { get; set; }

        [ForeignKey("Style")]
        public string style_id { get; set; }

        public Flower Flower { get; set; }
        public Style Style { get; set; }
    }

    public class Category
    {
        [Key]
        public string category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        // Mối quan hệ với bảng FlowerCategories
        public ICollection<FlowerCategory> FlowerCategories { get; set; }
    }

    // Lớp Occasion
    public class Occasion
    {
        [Key]
        public string occasion_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        // Mối quan hệ với bảng FlowerOccasions
        public ICollection<FlowerOccasion> FlowerOccasions { get; set; }
    }

    // Lớp Color
    public class ColorOfFlower
    {
        [Key]
        public string color_id { get; set; }
        public string name { get; set; }

        // Mối quan hệ với bảng FlowerColors
        public ICollection<FlowerColor> FlowerColors { get; set; }
    }

    // Lớp Style
    public class Style
    {
        [Key]
        public string style_id { get; set; }
        public string name { get; set; }

        // Mối quan hệ với bảng FlowerStyles
        public ICollection<FlowerStyle> FlowerStyles { get; set; }
    }
}
