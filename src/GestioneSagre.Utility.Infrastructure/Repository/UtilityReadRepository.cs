using GestioneSagre.Shared.GenericRepository;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Infrastructure.Repository;

public class UtilityReadRepository : GenericReadRepository<EmailMessage>, IUtilityReadRepository
{
    public UtilityReadRepository(ReadDbContext context) : base(context)
    {
    }
}