using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Username).HasMaxLength(100).IsRequired();

        builder.Property(x => x.Balance).IsRequired();

        builder.Property(x => x.IsAdmin).IsRequired();

        builder.Property(x => x.PasswordHash).IsRequired();
    }
}
