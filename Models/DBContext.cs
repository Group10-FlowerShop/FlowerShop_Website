using Microsoft.EntityFrameworkCore;

namespace FlowerShop_Website.Models
{
    public class DBContext
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            // Các DbSet tương ứng với các bảng trong cơ sở dữ liệu
            public DbSet<Flower> Flowers { get; set; }
            public DbSet<FlowerImage> FlowerImages { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<FlowerCategory> FlowerCategories { get; set; }
            public DbSet<Occasion> Occasions { get; set; }
            public DbSet<FlowerOccasion> FlowerOccasions { get; set; }
            public DbSet<ColorOfFlower> Colors { get; set; }
            public DbSet<FlowerColor> FlowerColors { get; set; }
            public DbSet<Style> Styles { get; set; }
            public DbSet<FlowerStyle> FlowerStyles { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Ánh xạ các bảng với các khoá ngoại
                modelBuilder.Entity<FlowerCategory>()
                    .ToTable("flower_categories")
                    .HasKey(fc => new { fc.flower_id, fc.category_id }); // Composite key
                modelBuilder.Entity<FlowerOccasion>()
                    .ToTable("flower_occasions")
                    .HasKey(fo => new { fo.flower_id, fo.occasion_id }); // Composite key
                modelBuilder.Entity<FlowerColor>()
                    .ToTable("flower_colors")
                    .HasKey(fc => new { fc.flower_id, fc.color_id }); // Composite key
                modelBuilder.Entity<FlowerStyle>()
                    .ToTable("flower_styles")
                    .HasKey(fs => new { fs.flower_id, fs.style_id }); // Composite key

                // Mối quan hệ giữa Flower và FlowerCategory (qua bảng FlowerCategory)
                modelBuilder.Entity<FlowerCategory>()
                    .HasOne(fc => fc.Flower)
                    .WithMany(f => f.FlowerCategories)
                    .HasForeignKey(fc => fc.flower_id);
                modelBuilder.Entity<FlowerCategory>()
                    .HasOne(fc => fc.Category)
                    .WithMany(c => c.FlowerCategories)
                    .HasForeignKey(fc => fc.category_id);

                // Mối quan hệ giữa Flower và FlowerOccasion (qua bảng FlowerOccasion)
                modelBuilder.Entity<FlowerOccasion>()
                    .HasOne(fo => fo.Flower)
                    .WithMany(f => f.FlowerOccasions)
                    .HasForeignKey(fo => fo.flower_id);
                modelBuilder.Entity<FlowerOccasion>()
                    .HasOne(fo => fo.Occasion)
                    .WithMany(o => o.FlowerOccasions)
                    .HasForeignKey(fo => fo.occasion_id);

                // Mối quan hệ giữa Flower và FlowerColor (qua bảng FlowerColor)
                modelBuilder.Entity<FlowerColor>()
                    .HasOne(fc => fc.Flower)
                    .WithMany(f => f.FlowerColors)
                    .HasForeignKey(fc => fc.flower_id);
                modelBuilder.Entity<FlowerColor>()
                    .HasOne(fc => fc.Color)
                    .WithMany(c => c.FlowerColors)
                    .HasForeignKey(fc => fc.color_id);

                // Mối quan hệ giữa Flower và FlowerStyle (qua bảng FlowerStyle)
                modelBuilder.Entity<FlowerStyle>()
                    .HasOne(fs => fs.Flower)
                    .WithMany(f => f.FlowerStyles)
                    .HasForeignKey(fs => fs.flower_id);
                modelBuilder.Entity<FlowerStyle>()
                    .HasOne(fs => fs.Style)
                    .WithMany(s => s.FlowerStyles)
                    .HasForeignKey(fs => fs.style_id);

                // Mối quan hệ giữa Flower và FlowerImage
                modelBuilder.Entity<Flower>()
                    .HasMany(f => f.FlowerImages)
                    .WithOne(fi => fi.Flower)
                    .HasForeignKey(fi => fi.flower_id);
            }
        }
    }
}
