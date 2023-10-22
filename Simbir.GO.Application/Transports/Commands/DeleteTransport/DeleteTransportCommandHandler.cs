using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Transports.Commands.DeleteTransport;

internal class DeleteTransportCommandHandler : IRequestHandler<DeleteTransportCommand, Error?>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransportCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Error?> Handle(DeleteTransportCommand request, CancellationToken cancellationToken)
    {
        var transport = await _unitOfWork.TransportRepository.FindById(request.TransportId);
        if (transport is null)
            return Errors.Transport.TransportDoesNotExist;

        if (transport.OwnerId != request.UserId)
            return Errors.Transport.OnlyOwnerCanDealWithHisTransport;

        _unitOfWork.TransportRepository.Remove(transport);
        await _unitOfWork.SaveChangesAsync();

        return null;
    }
}
