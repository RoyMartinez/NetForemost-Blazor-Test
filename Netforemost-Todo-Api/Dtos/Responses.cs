using Netforemost_Todo_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Netforemost_Todo_Api.Dtos
{

    public record TareasResponse(int Id,int IdUusario, int IdPrioridad,String Titulo,String Descripcion, DateTime FechaVencimiento,bool Finalizado,bool Eliminado, string Tags);
    public record TareasDetailedResponse(int Id, string Usuario, String Prioridad, String Titulo, string Descripcion, DateTime FechaVencimiento, bool Finalizado, bool Eliminado, string Tags, DateTime CreatedAt, DateTime UpdatedAt);
}
