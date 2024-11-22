using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Dto.Wager;
using OT.Assesment.Shared.Helpers;

namespace OT.Assessment.App.Interfaces
{
    public interface IPlayerService
    {
       Task<PagedListDto<WagerByPlayerDto>> GetPlayerWagers(Guid playerId, int pageSize, int page);
       Task<List<TopPlayersBySpendDto>> GetTopPlayerSpend(int count);
    }
}
