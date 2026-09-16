using Application.Contracts.Persistence;
using Persistence.Context;

namespace Persistence.UoW;

public sealed class UnitOfWork(SlowVibesDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
