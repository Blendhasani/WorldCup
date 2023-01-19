using Microsoft.AspNetCore.Mvc;
using WorldCup.Data.Base;
using WorldCup.Data.ViewModels;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
    public interface INewsService: IEntityBaseRepository<News>
    {
        Task<NewsDropdown> GetNewsDropdownValues();
        Task AddNewNewsAsync(News news);
        Task<News> GetNewsByIdAsync(int id);

        Task<News> DetailsN(int id);
        Task UpdateNews(int id, News news);

	}
}
