using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Shared.Repository;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbContext dbContext;

    protected GenericRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await dbContext.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await dbContext.Set<T>().FindAsync(id);

        if (entity == null)
        {
            return null;
        }

        dbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }

    public async Task<bool> CreateAsync(T entity)
    {
        dbContext.Set<T>().Add(entity);

        var result = await dbContext.SaveChangesAsync();

        dbContext.Entry(entity).State = EntityState.Detached;

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);

        var result = await dbContext.SaveChangesAsync();

        dbContext.Entry(entity).State = EntityState.Detached;

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);

        var result = await dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}