using Microsoft.AspNetCore.Mvc;
using Netforemost_Todo_Api.Dtos;
using Netforemost_Todo_Api.Models;
using Netforemost_Todo_Api.Services;

namespace Netforemost_Todo_Api.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/prioridad")]
    public class PrioridadController(IPrioridadService _Service) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> GetPrioridades(
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

        [HttpGet("{PrioridadId}")]
        public async Task<IActionResult> GetUsuarioById(
            [FromRoute] int PrioridadId)
        {
            Prioridad? Result = await _Service.GetByIdAsync(PrioridadId);

            if (Result is null)
                return NotFound();

            return Ok(Result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePrioridad(
            [FromBody] string Request)
        {
            Prioridad mapedRequest = Map(Request); 
            Prioridad? Result = await _Service.CreateAsync(mapedRequest);

            if (Result is null)
                return NotFound();

            return Ok(Result);
        }

        [HttpPut("{PrioridadId}")]
        public async Task<IActionResult> UpdateUsuario(
            [FromRoute] int PrioridadId,
            [FromBody] string Request)
        {
            Prioridad mapedRequest = MapWithId(PrioridadId, Request);
            Prioridad? Result = await _Service.UpdateAsync(mapedRequest);

            if (Result is null)
                return NotFound();

            return Ok(Result);
        }

        [HttpDelete("{PrioridadId}")]
        public async Task<IActionResult> DeleteUsuario(
            [FromRoute] int PrioridadId
        )
        {
            bool Result = await _Service.DeleteAsync(PrioridadId);

            if (!Result)
                return BadRequest("La prioridad no pudo ser Eliminado");

            return Ok(Result);
        }

        private Prioridad Map(string Request) => new Prioridad()
        {
            Nombre = Request
        };

        private Prioridad MapWithId(int UserId,string Request) => new Prioridad()
        {
            Id = UserId,
            Nombre = Request
        };
    }
}
