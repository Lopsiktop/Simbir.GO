using Mapster;
using Simbir.GO.Application.Transports.Commands.CreateTransport;
using Simbir.GO.Contracts.TransportContracts;

namespace Simbir.GO.WebAPI.Common.Mappings;

public class TransportMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(int OwnerId, TransportRequest Request), CreateTransportCommand>()
            .Map(dest => dest, source => source.Request)
            .Map(dest => dest.OwnerId, source => source.OwnerId);
    }
}
