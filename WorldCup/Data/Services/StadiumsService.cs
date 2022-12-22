using WorldCup.Data.Base;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
    public class StadiumsService : EntityBaseRepository<Stadium>, IStadiumsService
    {
        public StadiumsService(AppDbContext context) : base(context)
        {

        }
    }
}
