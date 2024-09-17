﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Netforemost_Todo_Api.Context;

#nullable disable

namespace Netforemost_Todo_Api.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("20240917052054_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Netforemost_Todo_Api.Models.Prioridad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prioridad");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Baja"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Media"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Alta"
                        });
                });

            modelBuilder.Entity("Netforemost_Todo_Api.Models.Tareas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Finalizado")
                        .HasColumnType("bit");

                    b.Property<int>("IdPrioridad")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdPrioridad");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Tareas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 9, 16, 23, 20, 54, 227, DateTimeKind.Local).AddTicks(3500),
                            Descripcion = "Crear .Net 8.0 API rest + Client Blazor app para demostrar habilidad en dichas herramientas",
                            Eliminado = false,
                            FechaVencimiento = new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Finalizado = true,
                            IdPrioridad = 1,
                            IdUsuario = 1,
                            Tags = "Full Stack, .Net 8.0, API Rest, Blazor",
                            Titulo = "Completar Test NetForemost",
                            UpdatedAt = new DateTime(2024, 9, 16, 23, 20, 54, 227, DateTimeKind.Local).AddTicks(3501)
                        });
                });

            modelBuilder.Entity("Netforemost_Todo_Api.Models.Usuarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Apellido = "Martinez Cano",
                            Correo = "roymartinez94@gmail.com",
                            CreatedAt = new DateTime(2024, 9, 16, 23, 20, 54, 227, DateTimeKind.Local).AddTicks(3471),
                            Nombre = "Roy Roger",
                            Telefono = "+50584892771",
                            UpdatedAt = new DateTime(2024, 9, 16, 23, 20, 54, 227, DateTimeKind.Local).AddTicks(3482)
                        });
                });

            modelBuilder.Entity("Netforemost_Todo_Api.Models.Tareas", b =>
                {
                    b.HasOne("Netforemost_Todo_Api.Models.Prioridad", "Prioridad")
                        .WithMany("Tareas")
                        .HasForeignKey("IdPrioridad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Netforemost_Todo_Api.Models.Usuarios", "Usuario")
                        .WithMany("Tareas")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prioridad");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Netforemost_Todo_Api.Models.Prioridad", b =>
                {
                    b.Navigation("Tareas");
                });

            modelBuilder.Entity("Netforemost_Todo_Api.Models.Usuarios", b =>
                {
                    b.Navigation("Tareas");
                });
#pragma warning restore 612, 618
        }
    }
}
