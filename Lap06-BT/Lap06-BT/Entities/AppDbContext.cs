using Lap06_BT.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap06_BT.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get;set; }
        public DbSet<Banner> Banners { get; set; }
    }
}
