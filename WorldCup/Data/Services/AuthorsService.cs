using WorldCup.Areas.Admin.Models;
using WorldCup.Data.Base;

namespace WorldCup.Data.Services
{
    public class AuthorsService : EntityBaseRepository<Author>, IAuthorsService
    {
        public AuthorsService(AppDbContext context) : base(context) { }
    }
}
