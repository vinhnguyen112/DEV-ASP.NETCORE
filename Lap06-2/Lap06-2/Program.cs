using Microsoft.EntityFrameworkCore;

namespace Lap06_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMvc();

            var connectionString = builder.Configuration.GetConnectionString("BookStoreConnectionString");

            // 2. Đăng ký BookStoreContext vào hệ thống Dependency Injection (DI)
            builder.Services.AddDbContext<Lap06_2.Models.BusinessModels.BookManagementContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                // 💡 Thêm chữ 's' thành controller=Books
                pattern: "{controller=Books}/{action=Index}/{id?}");



            app.Run();
        }
    }
}
