using Netforemost_Todo_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Netforemost_Todo_Api.Dtos;

public record PrioridadRequest(string Nombre);
public record UsuarioRequest(string Nombre,string Apellido,string Correo,string Telefono);
public record TareaRequest(int IdPrioridad, string Titulo, string Descripcion, DateTime FechaVencimiento, bool Finalizado, bool Eliminado,string Tags);