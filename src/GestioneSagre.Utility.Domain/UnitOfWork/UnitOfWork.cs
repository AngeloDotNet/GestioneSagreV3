using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Repository;

namespace GestioneSagre.Utility.Domain.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataDbContext dbContext;
    private readonly ReadDbContext readDbContext;
    public IUtilityReadRepository UtilityRead { get; }
    public IUtilityRepository UtilityWrite { get; }

    public UnitOfWork(DataDbContext dbContext, ReadDbContext readDbContext, IUtilityReadRepository utilityRead,
        IUtilityRepository utilityWrite)
    {
        this.dbContext = dbContext;
        this.readDbContext = readDbContext;

        UtilityRead = utilityRead;
        UtilityWrite = utilityWrite;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            dbContext.Dispose();
            readDbContext.Dispose();
        }
    }
}