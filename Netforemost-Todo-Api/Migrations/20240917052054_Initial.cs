using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Netforemost_Todo_Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prioridad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdPrioridad = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Prioridad_IdPrioridad",
                        column: x => x.IdPrioridad,
                        principalTable: "Prioridad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tareas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Prioridad",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Baja" },
                    { 2, "Media" },
                    { 3, "Alta" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Correo", "CreatedAt", "Nombre", "Telefono", "UpdatedAt" },
                values: new object[] { 1, "Martinez Cano", "roymartinez94@gmail.com", new DateTime(2024, 9, 16, 23, 20, 54, 227, DateTimeKind.Local).AddTicks(3471), "Roy Roger", "+50584892771", new DateTime(2024, 9, 16, 23, 20, 54, 227, DateTimeKind.Local).AddTicks(3482) });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "Eliminado", "FechaVencimiento", "Finalizado", "IdPrioridad", "IdUsuario", "Tags", "Titulo", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2024, 9, 16, 23, 20, 54, 227, DateTimeKind.Local).AddTicks(3500), "Crear .Net 8.0 API rest + Client Blazor app para demostrar habilidad en dichas herramientas", false, new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 1, "Full Stack, .Net 8.0, API Rest, Blazor", "Completar Test NetForemost", new DateTime(2024, 9, 16, 23, 20, 54, 227, DateTimeKind.Local).AddTicks(3501) });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdPrioridad",
                table: "Tareas",
                column: "IdPrioridad");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdUsuario",
                table: "Tareas",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Prioridad");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
