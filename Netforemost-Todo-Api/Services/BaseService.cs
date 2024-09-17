using Microsoft.EntityFrameworkCore;
using Netforemost_Todo_Api.Context;
using Netforemost_Todo_Api.Share;
using Netforemost_Todo_Api.Shared;

namespace Netforemost_Todo_Api.Services;
public interface IBaseService<T>
{
    public Task<PagedResult<T>> GetAllAsync(int? pageNumber, int? pageSize);

    public Task<T?> GetByIdAsync(int id);

    public  Task<T> CreateAsync(T entity);

    public  Task<T> UpdateAsync(T entity);

    public  Task<bool> DeleteAsync(int id);
}
public abstract class BaseService<T>: IBaseService<T> where T : class
{
    protected readonly IDbContextFactory<TodoContext> _dbContextFactory;
    protected readonly ILogger<BaseService<T>> _logger;

    protected BaseService(IDbContextFactory<TodoContext> dbContextFactory, ILogger<BaseService<T>> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    protected TodoContext CreateDbContext() => _dbContextFactory.CreateDbContext();

    public async Task<PagedResult<T>> GetAllAsync(int? pageNumber, int? pageSize)
    {
        try
        {
            using var context = CreateDbContext();
            var query = context.Set<T>();
            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while trying to retrieve {typeof(T).Name} entities.");
            throw new FetchException($"An error occurred while trying to retrieve {typeof(T).Name} entities.");
        }
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            using var context = CreateDbContext();
            T? entity = await context.Set<T>().FindAsync(id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while trying to retrieve a {typeof(T).Name} entity.");
            throw new FetchException($"An error occurred while trying to retrieve {typeof(T).Name} entities.");
        }
    }

    public async Task<T> CreateAsync(T entity)
    {
        try
        {
            using var context = CreateDbContext();
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while trying to create a {typeof(T).Name} entity.");
            throw new CreateException($"An error occurred while trying to create a {typeof(T).Name} entity.");
        }
    }

    public async Task<T> UpdateAsync(T entity)
    {
        try
        {
            using var context = CreateDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while trying to update a {typeof(T).Name} entity.");
            throw new UpdateException($"An error occurred while trying to update a {typeof(T).Name} entity.");
        }
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        try
        {
            using var context = CreateDbContext();
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while trying to delete a {typeof(T).Name} entity.");
            throw new RemoveException($"An error occurred while trying to delete a {typeof(T).Name} entity.");
        }
    }
}