using WorldCup.Data.Base;
using WorldCup.Models;

namespace WorldCup.Data.Services
{
	public interface IPlayersService
	{
		Task<IEnumerable<Player>> GetAllAsync();

		Task<Player> GetByIdAsync(int id);

		Task AddAsync(Player player);

		Task<Player> UpdateAsync(int id, Player newPlayer);

		Task DeleteAsync(int id);
	}
}
