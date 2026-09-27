using Lap06_BT.Models;
using Microsoft.EntityFrameworkCore;

namespace Lap06_BT.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Banner> Banners { get; set; }

        public DbSet<StdClass> StdClasses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Marks> Marks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính phức hợp cho bảng trung gian Marks (nhiều-nhiều giữa Student và Subjects)
            modelBuilder.Entity<Marks>()
                .HasKey(m => new { m.SubjectId, m.StudentId });

            // Cấu hình ràng buộc không trùng (Unique) cho Email và Phone của Student
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentEmail)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentPhone)
                .IsUnique();
        }
    }
}