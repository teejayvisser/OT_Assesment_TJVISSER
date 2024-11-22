using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OT.Assesment.EntityFrameworkCore.Models;

namespace OT.Assesment.EntityFrameworkCore.Repositories.ProviderRepository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly AppDbContext _context;

        public ProviderRepository(DbContextOptions<AppDbContext> options)
        {
            _context = new AppDbContext(options);
        }

        public async Task<Provider?> AddProvider(string name)
        {
            Provider provider = new Provider()
            {
                ProviderName = name
            };
            await _context.Providers.AddAsync(provider);
            await _context.SaveChangesAsync();
            return provider;
        }
        public async Task<Provider?> GetByProviderName( string providerName)
        {
            return await _context.Providers.Where(x => x.ProviderName.ToLower() == providerName.ToLower()).FirstOrDefaultAsync();
        }

    }
}