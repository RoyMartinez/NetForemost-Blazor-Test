using Microsoft.AspNetCore.Mvc;
using Netforemost_Todo_Api.Dtos;
using Netforemost_Todo_Api.Models;
using Netforemost_Todo_Api.Services;

namespace Netforemost_Todo_Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/usuario")]
public class UsuarioController(IUsuarioService _Service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetUsuarios(
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber)
    {
        var pagedResult = await _Service.GetAllAsync(pageNumber, pageSize);

        if (!pagedResult.Items.Any())
            return NotFound();

        return Ok(new
        {
            pagedResult.Items,
            Metadata = new
            {
                pagedResult.TotalCount,
                pagedResult.PageSize,
                pagedResult.PageNumber,
                pagedResult.TotalPages
            }
        });
    }

    [HttpGet("{UserId}")]
    public async Task<IActionResult> GetUsuarioById(
        [FromRoute] int UserId)
    {
        Usuarios? Result = await _Service.GetByIdAsync(UserId);

        if (Result is null)
            return NotFound();

        return Ok(Result);
    }

    [HttpPost()]
    public async Task<IActionResult> CreateUsuario(
        [FromBody] UsuarioRequest Request)
    {

        var mapedRequest = Map(Request);
        Usuarios? Result = await _Service.CreateAsync(mapedRequest);

        if (Result is null)
            return NotFound();

        return Ok(Result);
    }

    [HttpPut("{UserId}")]
    public async Task<IActionResult> UpdateUsuario(
        [FromRoute] int UserId,
        [FromBody] UsuarioRequest Request)
    {
        var mapedRequest = MapWithId(UserId, Request);
        Usuarios? Result = await _Service.UpdateAsync(mapedRequest);

        if (Result is null)
            return NotFound();

        return Ok(Result);
    }

    [HttpDelete("{UserId}")]
    public async Task<IActionResult> DeleteUsuario(
        [FromRoute] int UserId
    )
    {
        bool Result = await _Service.DeleteAsync(UserId);

        if (!Result)
            return BadRequest("El usuario no pudo ser Eliminado");

        return Ok(Result);
    }

    private Usuarios Map(UsuarioRequest Request)  => new Usuarios()
    {
        Nombre = Request.Nombre,
        Apellido = Request.Apellido,
        Correo = Request.Correo,
        Telefono = Request.Telefono,
    };

    private Usuarios MapWithId(int UserId,UsuarioRequest Request) => new Usuarios()
    {
        Id = UserId,
        Nombre = Request.Nombre,
        Apellido = Request.Apellido,
        Correo = Request.Correo,
        Telefono = Request.Telefono,
    };
}
