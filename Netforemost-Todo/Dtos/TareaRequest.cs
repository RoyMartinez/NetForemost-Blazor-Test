using Netforemost_Todo.Components.Pages.Usuario;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Netforemost_Todo.Dtos
{
    public class TareaResponse
    {
        public List<Tarea> Items { get; set; } = new List<Tarea>();
        public Metadata Metadata { get; set; } = new Metadata();
    }
    public class PrioridadResponse
    {
        public List<Prioridad> Items { get; set; } = new List<Prioridad>();
        public Metadata Metadata { get; set; } = new Metadata();
    }

    public class Tarea
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuarios? Usuario { get; set; } 
        public int IdPrioridad { get; set; }
        public Prioridad? Prioridad { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaVencimiento { get; set; }
        public bool Finalizado { get; set; } = false;
        public bool Eliminado { get; set; } = false;
        public string Tags { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class TareaRequest
    {
        public int Id { get; set; }
        public int IdPrioridad { get; set; }
        public Prioridad? Prioridad { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaVencimiento { get; set; }
        public bool Finalizado { get; set; } = false;
        public bool Eliminado { get; set; } = false;
        public string Tags { get; set; } = string.Empty;
    }
    public class Prioridad 
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty ;
    }
}
