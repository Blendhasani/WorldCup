using NuGet.DependencyResolver;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
    public interface IAccountService
    {
       
        Task RegisterAuthor(Author author);
    }
}
