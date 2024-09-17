using Microsoft.EntityFrameworkCore;
using Netforemost_Todo_Api.Context;
using Netforemost_Todo_Api.Models;

namespace Netforemost_Todo_Api.Services;
public interface IUsuarioService: IBaseService<Usuarios>{}
public class UsuarioService: BaseService<Usuarios>,IUsuarioService
{
    public UsuarioService(IDbContextFactory<TodoContext> dbContextFactory, ILogger<UsuarioService> logger)
        : base(dbContextFactory, logger)
    {
    }
}
