using Microsoft.EntityFrameworkCore;
using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Dto.Wager;
using OT.Assesment.EntityFrameworkCore.Repositories.WagerRepository;
using OT.Assessment.App.Interfaces;

namespace OT.Assessment.App.Services
{
    public class WagerService : IWagerService
    {
        public readonly IWagerRepository _wagerRepository;
        public WagerService(IWagerRepository wagerService)
        {
            _wagerRepository = wagerService;
        }
       


        public async Task<bool> CreateWager(CasinoWagerDto casinoWager)
        {
            var result = await _wagerRepository.CreateWager(casinoWager);
            return result;
        }

     
    }
}
