using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Dto.Wager;

namespace OT.Assessment.App.Interfaces
{
    public interface IWagerService
    {
        Task<bool> CreateWager(CasinoWagerDto casinoWager);
        
    }
}
