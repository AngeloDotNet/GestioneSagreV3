using GestioneSagre.Shared.GenericRepository;
using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Infrastructure.Repository;

public interface IUtilityRepository : IGenericWriteRepository<EmailMessage>
{
    // Add your custom code here
}