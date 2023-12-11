using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=tcp:dbdevelopmentserver.database.windows.net,1433;Initial Catalog=DBdev;Persist Security Info=False;User ID=sqladmin;Password=Sql@dmin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        public DbSet<User> Users { get; set; }
    }

}
