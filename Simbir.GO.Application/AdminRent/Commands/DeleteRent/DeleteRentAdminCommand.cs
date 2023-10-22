using ErrorOr;
using MediatR;

namespace Simbir.GO.Application.AdminRent.Commands.DeleteRent;

public record DeleteRentAdminCommand(int RentId, int UserId) : IRequest<Error?>;
