using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace FlowerShop_Website.Models
{
    [Table("flowers")]
    public class Flower
    {
        [Key]
        public string flower_id { get; set; }  // Khoá chính

        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string status { get; set; }

        // Mối quan hệ với bảng FlowerImage
        public ICollection<FlowerImage> FlowerImages { get; set; }

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

}
