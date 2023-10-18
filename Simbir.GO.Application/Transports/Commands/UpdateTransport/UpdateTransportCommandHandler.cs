using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Transports.Common;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Transports.Commands.UpdateTransport;

internal class UpdateTransportCommandHandler : IRequestHandler<UpdateTransportCommand, ErrorOr<TransportResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTransportCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<TransportResult>> Handle(UpdateTransportCommand request, CancellationToken cancellationToken)
    {
        var transport = await _unitOfWork.TransportRepository.FindById(request.TransportId);
        if (transport is null)
            return Errors.Transport.TransportDoesNotExist;

        if (transport.OwnerId != request.UserId)
            return Errors.Transport.OnlyOwnerCanDealWithHisTransport;

        var result = transport.Update(
            request.CanBeRented,
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
