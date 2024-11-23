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

            public DbSet<Flower> Flowers { get; set; }
            public DbSet<FlowerImage> FlowerImages { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Flower>()
             .HasMany(f => f.FlowerImages)
             .WithOne(fi => fi.Flower)
             .HasForeignKey(fi => fi.flower_id);
            }
        
        }
        
    }
}
