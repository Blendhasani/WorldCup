using WorldCup.Data.Base;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
    public class AuthorsService : EntityBaseRepository<Author>, IAuthorsService
    {
        public AuthorsService(AppDbContext context) : base(context) { }
    }
}
