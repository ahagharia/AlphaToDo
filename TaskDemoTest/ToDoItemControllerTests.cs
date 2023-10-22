using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TaskDemo.Controllers;
using TaskDemo.Data;
using TaskDemo.Models;

namespace TaskDemoTest;

public class ToDoItemControllerTests
{
    [Fact]
    public void GetAllTasks_ReturnsAllTasks()
    {
        var logger = new Mock<ILogger<HomeController>>();
        var toDoRepo = new Mock<IToDoItemRepository>();

        var homeController = new HomeController(logger.Object, toDoRepo.Object);

        //Act
        var result = homeController.Index();

        //Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void AddValidTodo_RedirectsToIndex()
    {
        var logger = new Mock<ILogger<HomeController>>();
        var toDoRepo = new Mock<IToDoItemRepository>();

       var homeController = new HomeController(logger.Object, toDoRepo.Object);

        var toDoItem = new ToDoItem()
        {
            Task = "TestTask"
        };
        //Act
        var result = homeController.Add(toDoItem);

        //Assert
        Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", ((RedirectToActionResult)result).ActionName);
    }
    [Fact]
    public void EditItem_ReturnsEditView()
    {

        var logger = new Mock<ILogger<HomeController>>();
        var toDoRepo = new Mock<IToDoItemRepository>();
        var item1 = new ToDoItem { Id = 1, Task = "ToDo1" };
        toDoRepo.Setup(repo => repo.GetById(item1.Id)).Returns(item1);

        var homeController = new HomeController(logger.Object, toDoRepo.Object);

        //Act
        var result = homeController.Edit(item1.Id);


        //Assert
        Assert.Equal(item1, ((ViewResult)result).Model);
    }

    [Fact]
    public void UpdateItem_RedirectsToIndex()
    {

        var logger = new Mock<ILogger<HomeController>>();
        var toDoRepo = new Mock<IToDoItemRepository>();
        var item1 = new ToDoItem { Id = 1, Task = "ToDo1" };
        toDoRepo.Setup(repo => repo.GetById(item1.Id)).Returns(item1);
        var homeController = new HomeController(logger.Object, toDoRepo.Object);

        //Act
        var result = homeController.Update(item1);

        //Assert
        Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", ((RedirectToActionResult)result).ActionName);
    }

    [Fact]
    public void DeleteItem_ReturnsToIndex()
    {
        // Arrange
        var logger = new Mock<ILogger<HomeController>>();
        var toDoRepo = new Mock<IToDoItemRepository>();
        var controller = new HomeController(logger.Object, toDoRepo.Object);

        var existingItem = new ToDoItem { Id = 1, Task = "Test Task" };
        toDoRepo.Setup(repo => repo.Delete(existingItem.Id)).Returns(true);

        // Act
        var result = controller.Delete(existingItem.Id);


        //Assert
        Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", ((RedirectToActionResult)result).ActionName);
    }
}



