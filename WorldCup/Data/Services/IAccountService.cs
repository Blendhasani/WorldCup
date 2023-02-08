using NuGet.DependencyResolver;
using WorldCup.Areas.Admin.Models;

namespace WorldCup.Data.Services
{
    public interface IAccountService
    {
       
        Task RegisterAuthor(Author author);
    }
}
