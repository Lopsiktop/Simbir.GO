﻿using Simbir.GO.Application.Common.Interfaces.Repositories;

namespace Simbir.GO.Application.Common.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }

    Task SaveChangesAsync();
}