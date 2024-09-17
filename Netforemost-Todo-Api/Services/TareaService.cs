using Microsoft.EntityFrameworkCore;
using Netforemost_Todo_Api.Context;
using Netforemost_Todo_Api.Dtos;
using Netforemost_Todo_Api.Models;
using Netforemost_Todo_Api.Share;
using Netforemost_Todo_Api.Shared;
using System.Threading.Tasks;

namespace Netforemost_Todo_Api.Services;
public interface ITareaService: IBaseService<Tareas>
{
    Task<PagedResult<TareasResponse>> GetAllFromUserAsync(int UserId, int? pageNumber, int? pageSize);
    Task<bool> DeleteLogicAsync(int id, int UserId);
}
public class TareaService: BaseService<Tareas>,ITareaService
{
    public TareaService(IDbContextFactory<TodoContext> dbContextFactory, ILogger<TareaService> logger)
        : base(dbContextFactory, logger)
    {
    }

    public async Task<PagedResult<TareasResponse>> GetAllFromUserAsync(int UserId,int? pageNumber, int? pageSize)
    {
        try
        {
            using var context = CreateDbContext();
            var query = context.Set<Tareas>()
                .Where(tarea => tarea.IdUsuario == UserId )
                .Select(tarea => new TareasResponse( 
                    tarea.Id,
                    tarea.IdUsuario,
                    tarea.IdPrioridad,
                    tarea.Titulo,
                    tarea.FechaVencimiento,
                    tarea.Finalizado,
                    tarea.Eliminado,
                    tarea.Tags
                ));
            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while trying to retrieve {typeof(Tareas).Name} entities.");
            throw new FetchException($"An error occurred while trying to retrieve {typeof(Tareas).Name} entities.");
        }
    }
    public override async Task<Tareas?> GetByIdAsync(int id)
    {
        try
        {
            using var context = CreateDbContext();
            Tareas? entity =  await context.Set<Tareas>()
                                     .Where(tarea => tarea.Id == id)
                                     .Include(tarea => tarea.Usuario)
                                     .Include(tarea => tarea.Prioridad)
                                     .FirstOrDefaultAsync();
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while trying to retrieve a {typeof(Tareas).Name} entity.");
            throw new FetchException($"An error occurred while trying to retrieve {typeof(Tareas).Name} entities.");
        }
    }

    public async Task<bool> DeleteLogicAsync(int id, int UserId)
    {
        try
        {
            using var context = CreateDbContext();
            var entity = await context.Set<Tareas>().FindAsync(id);
            if (entity is null) return false;
            if( entity.IdUsuario != UserId) return false;
            entity.Eliminado = true;
            entity.UpdateTimestamps();
            context.Set<Tareas>().Update(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while trying to delete a {typeof(Tareas).Name} entity.");
            throw new RemoveException($"An error occurred while trying to delete a {typeof(Tareas).Name} entity.");
        }
    }

}