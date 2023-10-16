using ErrorOr;
using MediatR;

namespace Simbir.GO.Application.AdminTransports.Commands.DeleteTransport;

public record DeleteTransportAdminCommand(int TransportId) : IRequest<Error?>;