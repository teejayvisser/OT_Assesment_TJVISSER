using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OT.Assesment.EntityFrameworkCore.Models;

namespace OT.Assesment.EntityFrameworkCore.Repositories.ProviderRepository
{
    public interface IProviderRepository
    {
        Task<Provider?> AddProvider(string name);

        Task<Provider?> GetByProviderName(string providerName);
    }
}
