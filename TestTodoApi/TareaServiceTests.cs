using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Netforemost_Todo_Api.Context;
using Netforemost_Todo_Api.Models;
using Netforemost_Todo_Api.Services;
using Microsoft.Extensions.Logging;
using Netforemost_Todo_Api.Share;
public class TareaServiceTests
{
    [Fact]
    public async Task GetAllFromUserAsync_NoTasks_ReturnsEmptyList()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_NoTasks")
            .Options;

        var mockContextFactory = new Mock<IDbContextFactory<TodoContext>>();
        var mockLogger = new Mock<ILogger<TareaService>>();
        var mockContext = new Mock<TodoContext>(options);

        int userId = 1;
        int? pageNumber = 1;
        int? pageSize = 10;

        var tareas = new List<Tareas>().AsQueryable();

        var mockSet = new Mock<DbSet<Tareas>>();
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Provider).Returns(tareas.Provider);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Expression).Returns(tareas.Expression);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.ElementType).Returns(tareas.ElementType);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.GetEnumerator()).Returns(tareas.GetEnumerator());

        mockContext.Setup(c => c.Set<Tareas>()).Returns(mockSet.Object);
        mockContextFactory.Setup(f => f.CreateDbContext()).Returns(mockContext.Object);

        var service = new TareaService(mockContextFactory.Object, mockLogger.Object);

        // Act
        var result = await service.GetAllFromUserAsync(userId, pageNumber, pageSize);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
    }
    [Fact]
    public async Task GetAllFromUserAsync_ExceptionThrown_LogsErrorAndThrowsFetchException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_Exception")
            .Options;

        var mockContextFactory = new Mock<IDbContextFactory<TodoContext>>();
        var mockLogger = new Mock<ILogger<TareaService>>();
        var mockContext = new Mock<TodoContext>(options);

        int userId = 1;
        int? pageNumber = 1;
        int? pageSize = 10;

        mockContext.Setup(c => c.Set<Tareas>()).Throws(new Exception("Database error"));
        mockContextFactory.Setup(f => f.CreateDbContext()).Returns(mockContext.Object);

        var service = new TareaService(mockContextFactory.Object, mockLogger.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<FetchException>(() => service.GetAllFromUserAsync(userId, pageNumber, pageSize));
        mockLogger.Verify(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
    }
    [Fact]
    public async Task GetAllFromUserAsync_Pagination_ReturnsCorrectPage()
    {
        // Arrange
        var mockContextFactory = new Mock<IDbContextFactory<TodoContext>>();
        var mockLogger = new Mock<ILogger<TareaService>>();
        var mockContext = new Mock<TodoContext>();

        int userId = 1;
        int? pageNumber = 2;
        int? pageSize = 1;

        var tareas = new List<Tareas>
    {
        new Tareas { Id = 1, IdUsuario = userId, IdPrioridad = 1, Titulo = "Task 1", Descripcion = "Description 1", FechaVencimiento = DateTime.Now, Finalizado = false, Eliminado = false, Tags = "tag1" },
        new Tareas { Id = 2, IdUsuario = userId, IdPrioridad = 2, Titulo = "Task 2", Descripcion = "Description 2", FechaVencimiento = DateTime.Now, Finalizado = false, Eliminado = false, Tags = "tag2" }
    }.AsQueryable();

        var mockSet = new Mock<DbSet<Tareas>>();
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Provider).Returns(tareas.Provider);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Expression).Returns(tareas.Expression);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.ElementType).Returns(tareas.ElementType);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.GetEnumerator()).Returns(tareas.GetEnumerator());

        mockContext.Setup(c => c.Set<Tareas>()).Returns(mockSet.Object);
        mockContextFactory.Setup(f => f.CreateDbContext()).Returns(mockContext.Object);

        var service = new TareaService(mockContextFactory.Object, mockLogger.Object);

        // Act
        var result = await service.GetAllFromUserAsync(userId, pageNumber, pageSize);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Items);
        Assert.Equal(2, result.Items.First().Id);
    }
    [Fact]
    public async Task GetAllFromUserAsync_RetrievesTasksForGivenUserId()
    {
        // Arrange
        var mockContextFactory = new Mock<IDbContextFactory<TodoContext>>();
        var mockLogger = new Mock<ILogger<TareaService>>();

        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var mockContext = new Mock<TodoContext>(options);

        int userId = 1;
        int? pageNumber = 1;
        int? pageSize = 10;

        var tareas = new List<Tareas>
        {
            new Tareas { Id = 1, IdUsuario = userId, IdPrioridad = 1, Titulo = "Task 1", Descripcion = "Description 1", FechaVencimiento = DateTime.Now, Finalizado = false, Eliminado = false, Tags = "tag1" },
            new Tareas { Id = 2, IdUsuario = userId, IdPrioridad = 2, Titulo = "Task 2", Descripcion = "Description 2", FechaVencimiento = DateTime.Now, Finalizado = false, Eliminado = false, Tags = "tag2" }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Tareas>>();
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Provider).Returns(tareas.Provider);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Expression).Returns(tareas.Expression);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.ElementType).Returns(tareas.ElementType);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.GetEnumerator()).Returns(tareas.GetEnumerator());

        mockContext.Setup(c => c.Set<Tareas>()).Returns(mockSet.Object);
        mockContextFactory.Setup(f => f.CreateDbContext()).Returns(mockContext.Object);

        var service = new TareaService(mockContextFactory.Object, mockLogger.Object);

        // Act
        var result = await service.GetAllFromUserAsync(userId, pageNumber, pageSize);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
    }
    [Fact]
    public async Task GetAllFromUserAsync_NoTasksForUser_ReturnsEmptyResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_NoTasks")
            .Options;

        var mockContextFactory = new Mock<IDbContextFactory<TodoContext>>();
        var mockLogger = new Mock<ILogger<TareaService>>();
        var mockContext = new Mock<TodoContext>(options);

        int userId = 1;
        int? pageNumber = 1;
        int? pageSize = 10;

        var tareas = new List<Tareas>().AsQueryable();

        var mockSet = new Mock<DbSet<Tareas>>();
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Provider).Returns(tareas.Provider);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Expression).Returns(tareas.Expression);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.ElementType).Returns(tareas.ElementType);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.GetEnumerator()).Returns(tareas.GetEnumerator());

        mockContext.Setup(c => c.Set<Tareas>()).Returns(mockSet.Object);
        mockContextFactory.Setup(f => f.CreateDbContext()).Returns(mockContext.Object);

        var service = new TareaService(mockContextFactory.Object, mockLogger.Object);

        // Act
        var result = await service.GetAllFromUserAsync(userId, pageNumber, pageSize);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
    }



    [Fact]
    public async Task GetAllFromUserAsync_DifferentPaginationValues_ReturnsCorrectPagedResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_Pagination")
            .Options;

        var mockContextFactory = new Mock<IDbContextFactory<TodoContext>>();
        var mockLogger = new Mock<ILogger<TareaService>>();
        var mockContext = new Mock<TodoContext>(options);

        int userId = 1;
        int? pageNumber = 2;
        int? pageSize = 1;

        var tareas = new List<Tareas>
    {
        new Tareas { Id = 1, IdUsuario = userId, IdPrioridad = 1, Titulo = "Task 1", Descripcion = "Description 1", FechaVencimiento = DateTime.Now, Finalizado = false, Eliminado = false, Tags = "tag1" },
        new Tareas { Id = 2, IdUsuario = userId, IdPrioridad = 2, Titulo = "Task 2", Descripcion = "Description 2", FechaVencimiento = DateTime.Now, Finalizado = false, Eliminado = false, Tags = "tag2" }
    }.AsQueryable();

        var mockSet = new Mock<DbSet<Tareas>>();
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Provider).Returns(tareas.Provider);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.Expression).Returns(tareas.Expression);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.ElementType).Returns(tareas.ElementType);
        mockSet.As<IQueryable<Tareas>>().Setup(m => m.GetEnumerator()).Returns(tareas.GetEnumerator());

        mockContext.Setup(c => c.Set<Tareas>()).Returns(mockSet.Object);
        mockContextFactory.Setup(f => f.CreateDbContext()).Returns(mockContext.Object);

        var service = new TareaService(mockContextFactory.Object, mockLogger.Object);

        // Act
        var result = await service.GetAllFromUserAsync(userId, pageNumber, pageSize);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Items);
        Assert.Equal(2, result.Items.First().Id);
    }
}