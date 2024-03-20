using Common.Domain.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistance;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext dbContext;

    public UnitOfWork(TContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}
