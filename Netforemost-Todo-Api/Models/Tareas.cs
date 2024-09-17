using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netforemost_Todo_Api.Models;

namespace Netforemost_Todo_Api.Models;
public class Tareas : AuditableEntity
{

    [Required]
    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }
    public Usuarios? Usuario { get; set; }

    [Required]
    [ForeignKey("Prioridad")]
    public int IdPrioridad { get; set; }
    public Prioridad? Prioridad { get; set; }

    [Required]
    [MaxLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    [Required]
    public DateTime FechaVencimiento { get; set; }

    public bool Finalizado { get; set; } = false;

    public bool Eliminado { get; set; } = false;

    [MaxLength(200)]
    public string Tags { get; set; } = string.Empty;
}