using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OT.Assesment.EntityFrameworkCore.Models;

namespace OT.Assesment.EntityFrameworkCore.Repositories.GameRepository
{
    public interface IGameRepository
    {
        Task<Game?> AddGame(string name,Guid providerId,string theme);
        Task<Game?> GetByGameName(string gameName);
    }
}
