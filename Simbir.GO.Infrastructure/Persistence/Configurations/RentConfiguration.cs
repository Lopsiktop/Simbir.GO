using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simbir.GO.Domain.RentEntity;

namespace Simbir.GO.Infrastructure.Persistence.Configurations;

public class RentConfiguration : IEntityTypeConfiguration<Rent>
{
    public void Configure(EntityTypeBuilder<Rent> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Renter)
            .WithMany()
            .HasForeignKey(x => x.RenterId)
            .IsRequired();

        builder.HasOne(x => x.Transport)
            .WithMany()
            .HasForeignKey(x => x.TransportId)
            .IsRequired();
    }
}
