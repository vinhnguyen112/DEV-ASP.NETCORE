using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Db = Lap06.Models.DBModel;

namespace Lap06_2.Models.BusinessModels
{
    public class BookManagementContext: DbContext
    {
        public BookManagementContext(DbContextOptions<BookManagementContext> options) : base(options) { }

        public DbSet<Db.Book> Books { get; set; }
        public DbSet<Db.Category> Categories { get; set; }
        public DbSet<Db.Publisher> Publishers { get; set; }
    }
}
