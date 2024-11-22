using Microsoft.AspNetCore.Mvc;
using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Dto.Wager;
using OT.Assesment.Shared.Helpers;
using OT.Assessment.App.Interfaces;

namespace OT.Assessment.App.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IWagerService _wagerService;
        private readonly IPlayerService _playerService;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(IWagerService wagerService, IPlayerService playerService,
            ILogger<PlayerController> logger)
        {
            _wagerService = wagerService;
            _playerService = playerService;
            _logger = logger;
        }
        //POST api/player/casinowager

        /// <summary>
        /// Adds new wagers to the rabbitMQ que
        /// </summary>
        /// <param name="casinoWager"></param>
        /// <returns></returns>
        [HttpPost("api/Player/casinowager")]
        public async Task<bool> CasinoWager(CasinoWagerDto casinoWager)
        {
            try
            {
                return await _wagerService.CreateWager(casinoWager);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred while processing message to CasinoWager queue : {e}");
                throw;
            }
        }

        //GET api/player/{playerId}/wagers
        /// <summary>
        /// Returns a paginated list of the latest casino wagers for a specific player
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("player/{playerId}/casino")]
        public async Task<PagedListDto<WagerByPlayerDto>> GetPlayerWagers(Guid playerId, int pageSize, int page)
        {
            try
            {
                return await _playerService.GetPlayerWagers(playerId, pageSize, page);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred while GetPlayerWagers was executed with playerId {playerId} : {e}");
                throw;
            }
        }


        //GET api/player/{playerId}/wagers
        /// <summary>
        /// Returns a list of the Top spending players by count
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("api/player/topSpenders")]
        public async Task<List<TopPlayersBySpendDto>> GetTopPlayerSpend(int count)
        {
            try
            {
                return await _playerService.GetTopPlayerSpend(count);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred while GetTopPlayerSpend was executed : {e}");
                throw;
            }
        }
    }
}