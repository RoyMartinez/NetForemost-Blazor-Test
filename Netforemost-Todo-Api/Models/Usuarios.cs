using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading;

namespace Netforemost_Todo_Api.Models;

/// <summary>
/// Representa un usuario en el sistema.
/// </summary>

public class Usuarios : AuditableEntity
{
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Correo { get; set; } = string.Empty;

    [Required]
    [MaxLength(15)]
    public string Telefono { get; set; } = string.Empty;

    public List<Tareas> Tareas { get; set; } = new List<Tareas>();
}