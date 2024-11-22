using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Dto.Wager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assesment.EntityFrameworkCore.Repositories.WagerRepository
{
    public interface IWagerRepository
    {
        Task<bool> CreateWager(CasinoWagerDto casinoWager);
        Task<bool> ConsumeWager(CasinoWagerDto casinoWagerDto);
    }
}
