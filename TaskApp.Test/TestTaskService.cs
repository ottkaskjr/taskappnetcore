using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskApp.Data;
using TaskApp.Services.Task;
using Xunit;

namespace TaskApp.Test
{
    public class TestTaskService
    {

        [Fact]
        public void TaskService_GetsAllTasks_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("gets_all").Options;

            using var context = new TaskDbContext(options);

            var sut = new TaskService(context);

            sut.Create(new Data.Models.Task { Id = 1 });
            sut.Create(new Data.Models.Task { Id = 2 });

            var allTasks = sut.GetAll();

            allTasks.Count.Should().Be(2);
        }

        [Fact]
        public void TaskService_CreateTask_GivenNewTaskObject()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("Add_writes_to_database").Options;

            using var context = new TaskDbContext(options);

            var sut = new TaskService(context);

            sut.Create(new Data.Models.Task { Id = 123 });

            context.Tasks.Single().Id.Should().Be(123);
        }

        [Fact]
        public void TaskService_DeleteTask_GivenNewTaskObject()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase("deletes_one").Options;

            using var context = new TaskDbContext(options);

            var sut = new TaskService(context);

            sut.Create(new Data.Models.Task { Id = 123 });

            sut.Delete(123);

            sut.GetAll().Count.Should().Be(0);
        }

        [Fact]
        public void TaskService_OrdersByDeadline_WhenGetAllTasksInvoked()
        {
            // Arrange
            var data = new List<Data.Models.Task>
            {
                new Data.Models.Task { Id = 123, Deadline = new DateTime(2022, 01, 01)},
                new Data.Models.Task { Id = 234, Deadline = DateTime.UtcNow},
                new Data.Models.Task { Id = 345, Deadline = new DateTime(2015, 12, 31)},
            }.AsQueryable();

            // nuget moq
            var mockSet = new Mock<DbSet<Data.Models.Task>>();

            mockSet.As<IQueryable<Data.Models.Task>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);

            mockSet.As<IQueryable<Data.Models.Task>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockSet.As<IQueryable<Data.Models.Task>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockSet.As<IQueryable<Data.Models.Task>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            var mockContext = new Mock<TaskDbContext>();


            mockContext.Setup(c => c.Tasks)
                .Returns(mockSet.Object);

            // Act
            var sut = new TaskService(mockContext.Object);
            var tasks = sut.GetAll();

            // Assert
            tasks.Count.Should().Be(3);
            // Make assertions about the order of the list
            tasks[0].Id.Should().Be(345);
            tasks[1].Id.Should().Be(234);
            tasks[2].Id.Should().Be(123);
        }
    }
}
