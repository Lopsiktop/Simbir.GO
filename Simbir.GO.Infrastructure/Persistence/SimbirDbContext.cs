using Microsoft.EntityFrameworkCore;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Infrastructure.Persistence;

public class SimbirDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } 

    public SimbirDbContext(DbContextOptions<SimbirDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimbirDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
