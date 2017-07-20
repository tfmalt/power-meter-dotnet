using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using PowerMeterApi;
using PowerMeterApi.Controllers;
using PowerMeterApi.Models;
using Microsoft.Extensions.Logging;

namespace PowerMeterApi.Tests
{
    public class PowerControllerTest
    {
        [Fact]
        public void GetAll_ReturnsListofTodoItems()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            optionsBuilder.UseInMemoryDatabase();
            var context = new TodoContext(optionsBuilder.Options);
            //private DbSet<TodoItem> todoitems =
            //{
            //    new TodoItem {Id = 1, Name = "Write a working test", IsComplete = false},
            //    new TodoItem {Id = 2, Name = "Start using xunit", IsComplete = true}
            //};
            
            var mockLogger = new Mock<ILogger<PowerController>>();
            var power = new PowerController(context, mockLogger.Object);
            var result = power.GetAll();

            Assert.IsType<List<TodoItem>>(result);
            Assert.Equal(1, result.Count());
            Assert.Equal("Item1", result.FirstOrDefault(t => t.Id == 1).Name);
        }

        [Fact]
        public void GetById_ReturnsAValidObjectResult()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            optionsBuilder.UseInMemoryDatabase();

            var context = new TodoContext(optionsBuilder.Options);
            var mockLogger = new Mock<ILogger<PowerController>>();

            var power = new PowerController(context, mockLogger.Object);
            var result = power.GetById(1);

            var obj = (ObjectResult) result;

            Assert.IsType<ObjectResult>(result);
            Assert.IsType<TodoItem>(obj.Value);
        }
    }
}
