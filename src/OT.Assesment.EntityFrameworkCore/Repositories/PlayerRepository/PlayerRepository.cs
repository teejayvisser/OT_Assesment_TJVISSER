using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Dto.Wager;
using OT.Assesment.EntityFrameworkCore.Models;
using OT.Assesment.Shared.Helpers;

namespace OT.Assesment.EntityFrameworkCore.Repositories.PlayerRepository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public PlayerRepository(DbContextOptions<AppDbContext> options, IMapper mapper)
        {
            _mapper = mapper;
            _context = new AppDbContext(options);
        }

        public async Task<Player?> GetByUserName(string username)
        {
            return await _context.Players.Where(x => x.Username.ToLower() == username.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<Player?> AddPlayer(string Username)
        {
            Player player = new Player()
            {
                Username = Username
            };
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return player;
        }



        public async Task<PagedListDto<WagerByPlayerDto>> GetPlayerWagers(Guid playerID, UserParams userParams)
        {
            var test = await _context.Wagers.Where(x => x.PlayerId == playerID).Include(x => x.Game).Include(x => x.Provider).ToListAsync();
            var wagers = _context.Wagers.Where(x => x.PlayerId == playerID).Include(x=>x.Game).Include(x=>x.Provider);
            return await PagedList<WagerByPlayerDto>.CreateAsync(
                wagers.ProjectTo<WagerByPlayerDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
            
        }


        public async Task<List<TopPlayersBySpendDto>> GetTopPlayerSpend(int count)
        {
            var result = await _context.TopPlayersBySpend.FromSqlInterpolated($"EXEC dbo.GetTopPlayersBySpend @Count = {count}").ToListAsync();
            return result;
        }

    }
}