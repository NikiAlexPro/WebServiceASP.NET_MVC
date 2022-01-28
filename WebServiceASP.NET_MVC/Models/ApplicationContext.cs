using Microsoft.EntityFrameworkCore;
namespace WebServiceASP.NET_MVC.Models
{
    public class ApplicationContext : DbContext
    {
        public readonly ILoggerFactory MyLoggerFactory;
        public DbSet<Product> Product { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        }

    }
}
