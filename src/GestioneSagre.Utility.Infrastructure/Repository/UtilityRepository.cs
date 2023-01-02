using GestioneSagre.Shared.GenericRepository;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Infrastructure.Repository;

public class UtilityRepository : GenericWriteRepository<EmailMessage>, IUtilityRepository
{
    public UtilityRepository(DataDbContext context) : base(context)
    {
    }
}