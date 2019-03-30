using Microsoft.EntityFrameworkCore;
using TestTask.DataAccess.Models;

namespace TestTask.DataAccess
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<UserLikedPage> UserLikedPages { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
