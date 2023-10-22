using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Rents.Common;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Rents.Queries.GetHistory;

internal class GetHistoryQueryHandler : IRequestHandler<GetHistoryQuery, ErrorOr<List<RentResult>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetHistoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<RentResult>>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AccountRepository.FindById(request.UserId);
        if (user is null)
            return Errors.Account.AccountDoesNotExist;

        var rents = await _unitOfWork.RentRepository.GetRentsByUserId(request.UserId);

        return rents.Select(x => new RentResult(x.Id, x.RenterId, x.TransportId, x.RentType.ToString(),
            x.TimeStart, x.TimeEnd, x.PriceOfUnit, x.FinalPrice)).ToList();
    }
}
