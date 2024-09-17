using Microsoft.AspNetCore.Mvc;
using Netforemost_Todo_Api.Dtos;
using Netforemost_Todo_Api.Models;
using Netforemost_Todo_Api.Services;

namespace Netforemost_Todo_Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/usuario/{UserId}/tareas")]
public class TareaController(ITareaService _Service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetTareas(
        [FromRoute] int UserId,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber)
    {
        var pagedResult = await _Service.GetAllFromUserAsync(UserId,pageNumber, pageSize);

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

    [HttpGet("{TareaId}")]
    public async Task<IActionResult> GetTareaById(
        [FromRoute] int TareaId)
    {
        Tareas? Result = await _Service.GetByIdAsync(TareaId);

        if (Result is null)
            return NotFound();

        return Ok(Result);
    }

    [HttpPost()]
    public async Task<IActionResult> CreateTarea(
        [FromRoute] int UserId,
        [FromBody] TareaRequest Request)
    {

        var mapedRequest = Map(Request,UserId);
        Tareas? Result = await _Service.CreateAsync(mapedRequest);

        if (Result is null)
            return NotFound();

        return Ok(Result);
    }

    [HttpPut("{TareaId}")]
    public async Task<IActionResult> UpdateTareas(
        [FromRoute] int UserId,
        [FromRoute] int TareaId,
        [FromBody] TareaRequest Request)
    {
        var mapedRequest = MapWithIds(UserId,TareaId,Request);
        Tareas? Result = await _Service.UpdateAsync(mapedRequest);

        if (Result is null)
            return NotFound();

        return Ok(Result);
    }

    [HttpDelete("{TareaId}")]
    public async Task<IActionResult> DeleteTareas(
        [FromRoute] int UserId,
        [FromRoute] int TareaId
    )
    {
        bool Result = await _Service.DeleteLogicAsync(TareaId,UserId);

        if (!Result)
            return BadRequest(""" El usuario no pudo ser Eliminado, verifique que el usuario que elimina sea el mismo que creo la tarea """);

        return Ok(Result);
    }

    private Tareas Map(TareaRequest Request,int UserId) => new Tareas()
    {
        Titulo = Request.Titulo,
        Descripcion = Request.Descripcion,
        FechaVencimiento = Request.FechaVencimiento,
        Finalizado = Request.Finalizado,
        Eliminado = Request.Eliminado,
        Tags = Request.Tags,
        IdPrioridad = Request.IdPrioridad,
        IdUsuario = UserId,
    };

    private Tareas MapWithIds(int UserId,int TareaId, TareaRequest Request) => new Tareas()
    {
        IdUsuario = UserId,
        Id = TareaId,
        Titulo = Request.Titulo,
        Descripcion = Request.Descripcion,
        FechaVencimiento = Request.FechaVencimiento,
        Finalizado = Request.Finalizado,
        Eliminado = Request.Eliminado,
        Tags = Request.Tags
    };
}
