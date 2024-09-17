using Microsoft.EntityFrameworkCore;
using Netforemost_Todo_Api.Models;

namespace Netforemost_Todo_Api.Context;

public class TodoContext : DbContext
{
    public DbSet<Prioridad> Prioridad => Set<Prioridad>();
    public DbSet<Usuarios> Usuarios => Set<Usuarios>();
    public DbSet<Tareas> Tareas => Set<Tareas>();

    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Prioridad>()
            .HasMany(u => u.Tareas)
            .WithOne(t => t.Prioridad)
            .HasForeignKey(t => t.IdPrioridad)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.Tareas)
            .WithOne(t => t.Usuario)
            .HasForeignKey(t => t.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Prioridad>().HasData(
            new Prioridad { Id = 1, Nombre = "Baja" },
            new Prioridad { Id = 2, Nombre = "Media" },
            new Prioridad { Id = 3, Nombre = "Alta" }
        );

        modelBuilder.Entity<Usuarios>().HasData(
            new Usuarios { 
                Id = 1,
                Nombre = "Roy Roger",
                Apellido = "Martinez Cano",
                Correo = "roymartinez94@gmail.com",
                Telefono = "+50584892771"
            }
        );
        modelBuilder.Entity<Tareas>().HasData(
            new Tareas { 
                Id = 1,
                IdUsuario=1,
                IdPrioridad=1,
                Titulo = "Completar Test NetForemost",
                Descripcion= "Crear .Net 8.0 API rest + Client Blazor app para demostrar habilidad en dichas herramientas",
                FechaVencimiento= new DateTime(2024,09,17),
                Finalizado=true,
                Eliminado=false,
                Tags="Full Stack, .Net 8.0, API Rest, Blazor"
            }
        );
    }
}
