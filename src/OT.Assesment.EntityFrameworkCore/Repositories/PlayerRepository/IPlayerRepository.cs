using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Dto.Wager;
using OT.Assesment.EntityFrameworkCore.Models;
using OT.Assesment.Shared.Helpers;

namespace OT.Assesment.EntityFrameworkCore.Repositories.PlayerRepository
{
    public interface IPlayerRepository
    {
      Task<Player?> GetByUserName(string Username);
      
      Task<Player?> AddPlayer(string Username);

      Task<PagedListDto<WagerByPlayerDto>> GetPlayerWagers(Guid playerID, UserParams userParams);

      Task<List<TopPlayersBySpendDto>> GetTopPlayerSpend(int count);

    }
}
