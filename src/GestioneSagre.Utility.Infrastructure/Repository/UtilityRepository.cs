using GestioneSagre.Shared.Repository;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Infrastructure.Repository;

public class UtilityRepository : GenericRepository<EmailMessage>, IUtilityRepository
{
    public UtilityRepository(DataDbContext context) : base(context)
    {
    }
}