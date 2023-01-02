namespace GestioneSagre.Shared.GenericRepository;

public interface IGenericReadRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
}