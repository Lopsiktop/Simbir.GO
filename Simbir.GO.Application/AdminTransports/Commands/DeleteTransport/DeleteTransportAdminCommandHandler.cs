using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.AdminTransports.Commands.DeleteTransport;

internal class DeleteTransportAdminCommandHandler : IRequestHandler<DeleteTransportAdminCommand, Error?>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransportAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Error?> Handle(DeleteTransportAdminCommand request, CancellationToken cancellationToken)
    {
        var transport = await _unitOfWork.TransportRepository.FindById(request.TransportId);
        if (transport is null)
            return Errors.Transport.TransportDoesNotExist;

        _unitOfWork.TransportRepository.Remove(transport);
        await _unitOfWork.SaveChangesAsync();

        return null;
    }
}
