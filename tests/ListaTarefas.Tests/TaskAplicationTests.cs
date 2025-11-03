using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using ListaTarefas.Aplication.tasks;
using ListaTarefas.Infra.repository.interfaces;
using ListaTarefas.Domain;
using ListTarefas.Communication.request;
using ListTarefas.Communication.response;
using MapsterMapper;
using Moq;
using Xunit;

public class TaskAplicationTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ITaskRepository> _taskRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private readonly TaskAplication _service;

    public TaskAplicationTests()
    {
        _service = new TaskAplication(_mapperMock.Object, null, _taskRepoMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateTask_ShouldAddAndCommit()
    {
        // Arrange
        var request = new CreateRequestTaskJson { Title = "Teste", Description = "Descrição" };
        var userTask = new UserTask(request.Title, request.Description);

        _mapperMock.Setup(m => m.Map<UserTask>(request)).Returns(userTask);

        // Act
        var result = await _service.CreateTask(request);

        // Assert
        _taskRepoMock.Verify(r => r.AddTask(It.IsAny<UserTask>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task GetAllTask_ShouldReturnMappedTasks()
    {
        // Arrange
        var tasks = new List<UserTask> { new("T1", "D1") { } };
        var response = new List<ResponseTask> { new() { title = "T1", description = "D1" } };

        _taskRepoMock.Setup(r => r.GetAllTask()).ReturnsAsync(tasks);
        _mapperMock.Setup(m => m.Map<List<ResponseTask>>(tasks)).Returns(response);

        // Act
        var result = await _service.GetAllTask();

        // Assert
        Assert.Single(result);
        Assert.Equal("T1", result[0].title);
    }

    [Fact]
    public async Task UpdateTask_ShouldToggleStatusAndCommit()
    {
        // Arrange
        var id = Guid.NewGuid();
        var task = new UserTask("T1","D1");
        task.MarkAsDone();

        _taskRepoMock.Setup(r => r.GetTask(id)).ReturnsAsync(task);

        // Act
        await _service.UpdateTask(id);

        // Assert
        _taskRepoMock.Verify(r => r.UpdateTask(task), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
    }

    [Fact]
    public async Task DeleteTask_ShouldRemoveAndCommit()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        await _service.DeleteTask(id);

        // Assert
        _taskRepoMock.Verify(r => r.DeleteTask(id), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
    }
}