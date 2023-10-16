using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Transports.Common;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.AdminTransports.Commands.UpdateTransport;

internal class UpdateTransportAdminCommandHandler : IRequestHandler<UpdateTransportAdminCommand, ErrorOr<TransportResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTransportAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<TransportResult>> Handle(UpdateTransportAdminCommand request, CancellationToken cancellationToken)
    {
        var transport = await _unitOfWork.TransportRepository.FindById(request.TransportId);
        if (transport is null)
            return Errors.Transport.TransportDoesNotExist;

        var owner = await _unitOfWork.AccountRepository.FindById(request.OwnerId);
        if (owner is null)
            return Errors.Account.AccountDoesNotExist;

        var result = transport.Update(
            owner,
            request.CanBeRented,
            request.TransportType,
            request.Model,
            request.Color,
            request.Identifier,
            request.Description,
            request.Latitude,
            request.Longitude,
            request.MinutePrice,
            request.DayPrice);

        if (result.Count != 0)
            return result;

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
            request.Longitude,
            request.MinutePrice,
            request.DayPrice);
    }
}
