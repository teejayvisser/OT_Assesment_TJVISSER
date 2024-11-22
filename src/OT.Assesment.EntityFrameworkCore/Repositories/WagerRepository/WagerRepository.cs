using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Models;
using OT.Assesment.EntityFrameworkCore.Repositories.PlayerRepository;
using OT.Assesment.Shared.RabbitMq;
using OT.Assessment.Consumer.Models;
using OT.Assesment.EntityFrameworkCore.Repositories.GameRepository;
using OT.Assesment.EntityFrameworkCore.Repositories.ProviderRepository;

namespace OT.Assesment.EntityFrameworkCore.Repositories.WagerRepository
{
    public class WagerRepository : IWagerRepository
    {
        private readonly AppDbContext _context;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly ILogger<WagerRepository> _logger;

        private readonly IRabbitMQService<CasinoWagerDto> _rabbitMqService;


        public WagerRepository(DbContextOptions<AppDbContext> options, IRabbitMQService<CasinoWagerDto> rabbitMqService,
            IPlayerRepository playerRepository, IGameRepository gameRepository, IProviderRepository providerRepository, ILogger<WagerRepository> logger)
        {
            _context = new AppDbContext(options);
            _rabbitMqService = rabbitMqService;
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _providerRepository = providerRepository;
            _logger = logger;
        }

        public async Task<bool> CreateWager(CasinoWagerDto casinoWager)
        {
            try
            {
                await _rabbitMqService.PublishMessageAsync(casinoWager, MessageBroker.RabbitMQQueues.WagerQue);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred while PublishMessageAsync to CasinoWager queue : {e}");
                return false;
                throw;
            }
        }


        public async Task<bool> ConsumeWager(CasinoWagerDto casinoWagerDto)
        {
            //create wager 
            try
            {
                //createPlayer if not exist
                var existingPlayer = await _playerRepository.GetByUserName(casinoWagerDto.Username) ??
                                     await _playerRepository.AddPlayer(casinoWagerDto.Username);

                //check if provider exist
                var existingProvider = await _providerRepository.GetByProviderName(casinoWagerDto.Provider) ??
                                       await _providerRepository.AddProvider(casinoWagerDto.Provider);

                //check if game exists
                var existingGame = await _gameRepository.GetByGameName(casinoWagerDto.GameName) ??
                                   await _gameRepository.AddGame(casinoWagerDto.GameName, existingProvider.Id,
                                       casinoWagerDto.Theme);

                //create wager 
                var existingWager = await _context.Wagers.Where(
                    x => x.CreatedDateTime == casinoWagerDto.CreatedDateTime &&
                         existingPlayer.Username == casinoWagerDto.Username).FirstOrDefaultAsync();
                if (existingWager == null)
                {
                    Wager wager = new Wager()
                    {
                        ProviderId = existingProvider.Id,
                        GameId = existingGame.Id,
                        Amount = casinoWagerDto.Amount,
                        PlayerId = existingPlayer.Id,
                        CreatedDateTime = casinoWagerDto.CreatedDateTime,
                        NumberOfBets = casinoWagerDto.NumberOfBets,
                    };

                    await _context.Wagers.AddAsync(wager);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            //create 
        }
    }
}