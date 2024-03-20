using CoinList.Domain.CoinEntity;
using Common.Infrastructure.Schemas;
using Microsoft.EntityFrameworkCore;

namespace CoinList.Infrastrcture;

public class CoinListDbContext : DbContext
{
    public DbSet<Coin> Coin { get; set; }

    public CoinListDbContext(DbContextOptions<CoinListDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbSchema.CoinList);
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}

