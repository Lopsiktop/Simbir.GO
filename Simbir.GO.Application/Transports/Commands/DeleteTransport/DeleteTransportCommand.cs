using ErrorOr;
using MediatR;

namespace Simbir.GO.Application.Transports.Commands.DeleteTransport;

public record DeleteTransportCommand(int TransportId, int UserId) : IRequest<Error?>;