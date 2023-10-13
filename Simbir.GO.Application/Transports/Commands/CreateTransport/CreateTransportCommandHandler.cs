using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Transports.Common;
using Simbir.GO.Domain.Common.Errors;
using Simbir.GO.Domain.TransportEntity;

namespace Simbir.GO.Application.Transports.Commands.CreateTransport;

internal class CreateTransportCommandHandler : IRequestHandler<CreateTransportCommand, ErrorOr<TransportResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransportCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<TransportResult>> Handle(CreateTransportCommand request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.AccountRepository.FindById(request.OwnerId);

        if (account is null)
            return Errors.Account.AccountDoesNotExist;

        var transportResult = Transport.Create(account, request.CanBeRented, request.TransportType, request.Model, request.Color, request.Identifier, request.Description, request.Latitude, request.Longitude, request.MinutePrice, request.DayPrice);
        if (transportResult.IsError)
            return transportResult.Errors;

        var transport = transportResult.Value;
        
        await _unitOfWork.TransportRepository.Add(transport);
        await _unitOfWork.SaveChangesAsync();

        return new TransportResult(
            transport.Id,
            transport.OwnerId,
            transport.CanBeRented,
            transport.TransportType.ToString(),
            transport.Model,
            transport.Color,
            transport.Identifier,
            transport.Description,
            transport.Latitude,
            transport.Longitude,
            transport.MinutePrice,
            transport.DayPrice);
    }
}
