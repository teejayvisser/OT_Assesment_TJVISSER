using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OT.Assesment.EntityFrameworkCore.Models;

namespace OT.Assesment.EntityFrameworkCore.Repositories.GameRepository
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(DbContextOptions<AppDbContext> options)
        {
            _context = new AppDbContext(options);
        }

        public async Task<Game?> AddGame(string name ,Guid providerId,string theme)
        {
            Game game = new Game()
            {
                Name = name,
                ProviderId = providerId,
                Theme = theme
            };
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<Game?> GetByGameName(string gameName)
        {
            return await _context.Games.Where(x => x.Name.ToLower() == gameName.ToLower()).FirstOrDefaultAsync();
        }
    }
}

