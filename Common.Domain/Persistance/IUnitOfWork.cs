namespace Common.Domain.Persistance;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
