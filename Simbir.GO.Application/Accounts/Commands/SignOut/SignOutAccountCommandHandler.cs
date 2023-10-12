﻿using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Application.Accounts.Commands.SignOut;

internal class SignOutAccountCommandHandler : IRequestHandler<SignOutAccountCommand, Error?>
{
    private readonly IUnitOfWork _unitOfWork;

    public SignOutAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Error?> Handle(SignOutAccountCommand request, CancellationToken cancellationToken)
    {
        var tokenResult = RevokedToken.Create(request.Token);

        if (tokenResult.IsError)
            return tokenResult.FirstError;

        var token = tokenResult.Value;

        await _unitOfWork.RevokedTokenRepository.Add(token);
        await _unitOfWork.SaveChangesAsync();

        return null;
    }
}
