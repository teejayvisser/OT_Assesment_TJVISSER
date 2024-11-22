using System.Drawing;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OT.Assesment.EntityFrameworkCore.Dto.Player;
using OT.Assesment.EntityFrameworkCore.Models;

namespace OT.Assesment.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Player?> Players { get; set; }
    public DbSet<Wager> Wagers { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Provider> Providers { get; set; }

    public DbSet<TopPlayersBySpendDto> TopPlayersBySpend { get; set; } //this is added in order to be able to call the stored procedure via entity framework
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        modelBuilder.Entity<TopPlayersBySpendDto>(entity => 
            entity.HasKey(e => e.AccountId)); 
    }
}