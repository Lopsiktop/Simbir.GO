﻿using ErrorOr;
using MediatR;
using Simbir.GO.Application.Rents.Common;

namespace Simbir.GO.Application.Rents.Commands.EndRent;

public record EndRentAdminCommand(int RentId, int UserId, double Lat, double Long)
    : IRequest<ErrorOr<RentResult>>;
