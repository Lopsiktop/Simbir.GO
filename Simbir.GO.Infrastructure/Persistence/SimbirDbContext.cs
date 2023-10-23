using Microsoft.EntityFrameworkCore;
using Simbir.GO.Domain.AccountEntity;
using Simbir.GO.Domain.RentEntity;
using Simbir.GO.Domain.TransportEntity;

namespace Simbir.GO.Infrastructure.Persistence;

public class SimbirDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } 

    public DbSet<RevokedToken> RevokedTokens { get; set; }

    public DbSet<Transport> Transports { get; set; }

    public DbSet<Rent> Rents { get; set; }

    public SimbirDbContext(DbContextOptions<SimbirDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimbirDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
