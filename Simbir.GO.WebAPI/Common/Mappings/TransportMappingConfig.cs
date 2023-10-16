using Mapster;
using Simbir.GO.Application.AdminTransports.Commands.DeleteTransport;
using Simbir.GO.Application.AdminTransports.Commands.UpdateTransport;
using Simbir.GO.Application.Transports.Commands.CreateTransport;
using Simbir.GO.Application.Transports.Commands.UpdateTransport;
using Simbir.GO.Contracts.TransportContracts;

namespace Simbir.GO.WebAPI.Common.Mappings;

public class TransportMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(int OwnerId, TransportRequest Request), CreateTransportCommand>()
            .Map(dest => dest, source => source.Request)
            .Map(dest => dest.OwnerId, source => source.OwnerId);

        config.NewConfig<(int UserId, int TransportId, UpdateTransportRequest Request), UpdateTransportCommand>()
            .Map(dest => dest, src => src.Request)
            .Map(dest => dest.TransportId, src => src.TransportId)
            .Map(dest => dest.UserId, src => src.UserId);

        config.NewConfig<(int TransportId, UpdateTransportAdminRequest Request), UpdateTransportAdminCommand>()
            .Map(dest => dest, src => src.Request)
            .Map(dest => dest.TransportId, src => src.TransportId);
    }
}
