using Bookliz_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookliz_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define DbSet properties for your entities here
        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public object Users { get; internal set; } = new object();
    }
}