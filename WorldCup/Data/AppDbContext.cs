using Microsoft.EntityFrameworkCore;
using WorldCup.Models;

namespace WorldCup.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Highlights> Highlights { get; set; }
        public DbSet<News> News { get; set; }
		public DbSet<Author> Authors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stadium> Stadiums { get; set;}
	}
}
