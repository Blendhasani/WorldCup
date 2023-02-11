using Microsoft.EntityFrameworkCore;
using WorldCup.Areas.Admin.Models;
using WorldCup.Data.ViewModels;

namespace WorldCup.Data.Services
{
    public class PlayersService : IPlayersService
	{
		private readonly AppDbContext _context;
		public PlayersService(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Player player)
		{
			await _context.Players.AddAsync(player);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var result = await _context.Players.FirstOrDefaultAsync(n => n.Id == id);
			_context.Players.Remove(result);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Player>> GetAllAsync()
		{
			var result = await _context.Players.ToListAsync();
			return result;
		}

		public async Task<Player> GetByIdAsync(int id)
		{
			var result = await _context.Players.FirstOrDefaultAsync(n => n.Id == id);
			return result;
		}

		public async Task<PlayerDropdown> GetPlayerDropdownValues()
		{
			var response = new PlayerDropdown()
			{
				Clubs = await _context.Clubs.OrderBy(n => n.Name).ToListAsync()
			};
			return response;
		}

		public async Task<Player> UpdateAsync(int id, Player newPlayer)
		{
			_context.Update(newPlayer);
			await _context.SaveChangesAsync();
			return newPlayer;
		}

		public async Task<Player> GetPlayerByIdAsync(int id)
		{
			var playerDetails = await _context.Players
				.Include(c => c.Club)
				.FirstOrDefaultAsync(n => n.Id == id);

			return playerDetails;
		}
	}
}
