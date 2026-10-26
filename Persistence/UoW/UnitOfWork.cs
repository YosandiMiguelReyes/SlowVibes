using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Domain.Exceptions;

namespace Persistence.UoW;

public sealed class UnitOfWork(SlowVibesDbContext context) : IUnitOfWork
{

    public Task<int> SaveChangesAsync()
    {
        try{
            return context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            throw new ConcurrencyException(
                "Ocurrió un error de concurrencia al guardar los cambios en la base de datos. " +
                "Es posible que otro usuario haya modificado los mismos datos. " +
                "Por favor, actualice la información y vuelva a intentarlo.");
        }
    }
}
