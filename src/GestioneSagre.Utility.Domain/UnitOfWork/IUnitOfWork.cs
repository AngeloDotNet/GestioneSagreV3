using GestioneSagre.Utility.Infrastructure.Repository;

namespace GestioneSagre.Utility.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUtilityReadRepository UtilityRead { get; }
    IUtilityRepository UtilityWrite { get; }
}