using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Rents.Common;
using Simbir.GO.Domain.Common.Errors;
using Simbir.GO.Domain.RentEntity;

namespace Simbir.GO.Application.Rents.Commands.NewRent;

internal class NewRentCommandHandler : IRequestHandler<NewRentCommand, ErrorOr<RentResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public NewRentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<RentResult>> Handle(NewRentCommand request, CancellationToken cancellationToken)
    {
        var renter = await _unitOfWork.AccountRepository.FindById(request.RenterId);
        if (renter is null)
            return Errors.Account.AccountDoesNotExist;

        var transport = await _unitOfWork.TransportRepository.FindById(request.TransportId);
        if (transport is null)
            return Errors.Transport.TransportDoesNotExist;

        var rentResult = Rent.Create(renter, transport, request.RentType, DateTime.UtcNow, null, null, null);
        if (rentResult.IsError)
            return rentResult.Errors;

        var rent = rentResult.Value;

        var add_result = await _unitOfWork.RentRepository.Add(rent);
        if (add_result.HasValue)
            return add_result.Value;

        await _unitOfWork.SaveChangesAsync();

        return new RentResult(rent.Id, rent.RenterId, rent.TransportId, rent.RentType.ToString(),
            rent.TimeStart, rent.TimeEnd, rent.PriceOfUnit, rent.FinalPrice);
    }
}
