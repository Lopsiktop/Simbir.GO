using Mapster;
using Simbir.GO.Application.AdminRent.Commands.NewRent;
using Simbir.GO.Contracts.RentContracts;

namespace Simbir.GO.WebAPI.Common.Mappings;

public class RentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(int Id, RentAdminRequest Request), UpdateRentAdminCommand>()
            .Map(dest => dest, src => src.Request)
            .Map(dest => dest.RentId, src => src.Id);
    }
}