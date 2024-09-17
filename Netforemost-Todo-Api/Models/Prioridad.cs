namespace Netforemost_Todo_Api.Models;

public class Prioridad:BaseEntity
{
    public required string Nombre { get; set; } = string.Empty;
    public List<Tareas> Tareas { get; set; } = new List<Tareas>();
}
