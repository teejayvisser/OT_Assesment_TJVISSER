using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Dto.Wager;
using OT.Assesment.EntityFrameworkCore.Repositories.PlayerRepository;
using OT.Assesment.Shared.Helpers;
using OT.Assessment.App.Interfaces;

namespace OT.Assessment.App.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PagedListDto<WagerByPlayerDto>> GetPlayerWagers(Guid playerId, int pageSize, int page)
        {
            var userParams = new UserParams()
            {
                PageNumber = page,
                PageSize = pageSize
            };
            var result = await _playerRepository.GetPlayerWagers(playerId, userParams);
            return result;
        }


        public async Task<List<TopPlayersBySpendDto>> GetTopPlayerSpend(int count)
        {
            return await _playerRepository.GetTopPlayerSpend(count);
        }
    }
}