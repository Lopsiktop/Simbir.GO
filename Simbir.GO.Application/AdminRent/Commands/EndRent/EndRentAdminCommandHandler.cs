﻿using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Rents.Common;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Rents.Commands.EndRent;

internal class EndRentAdminCommandHandler : IRequestHandler<EndRentAdminCommand, ErrorOr<RentResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public EndRentAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<RentResult>> Handle(EndRentAdminCommand request, CancellationToken cancellationToken)
    {
        var rent = await _unitOfWork.RentRepository.FindByIdInclude(request.RentId);
        if (rent is null)
            return Errors.Rent.RentDoesNotExist;

        var user = await _unitOfWork.AccountRepository.FindById(request.UserId);
        if (user is null)
            return Errors.Account.AccountDoesNotExist;

        if (!user.IsAdmin)
            return Errors.General.OnlyForAdmin;

        rent.Transport.UpdateLocation(request.Lat, request.Long);

        var result = rent.EndRent();
        if (result.HasValue)
            return result.Value;

        await _unitOfWork.SaveChangesAsync();

        return new RentResult(rent.Id, rent.RenterId, rent.TransportId, rent.RentType.ToString(),
            rent.TimeStart, rent.TimeEnd, rent.PriceOfUnit, rent.FinalPrice);
    }
}
