using Microsoft.EntityFrameworkCore;
using Netforemost_Todo_Api.Context;
using Netforemost_Todo_Api.Models;

namespace Netforemost_Todo_Api.Services;
public interface IPrioridadService: IBaseService<Prioridad>{}
public class PrioridadService: BaseService<Prioridad>,IPrioridadService
{
    public PrioridadService(IDbContextFactory<TodoContext> dbContextFactory, ILogger<PrioridadService> logger)
        : base(dbContextFactory, logger)
    {
    }
}