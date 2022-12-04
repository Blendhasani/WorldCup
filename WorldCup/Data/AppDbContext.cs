using Microsoft.EntityFrameworkCore;

namespace WorldCup.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
