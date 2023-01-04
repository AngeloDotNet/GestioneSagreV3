using GestioneSagre.Shared.Repository;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Infrastructure.Repository;

public class UtilityReadRepository : GenericRepository<EmailMessage>, IUtilityReadRepository
{
    public UtilityReadRepository(ReadDbContext context) : base(context)
    {
    }
}