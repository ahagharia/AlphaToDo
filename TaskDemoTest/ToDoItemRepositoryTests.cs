using Microsoft.EntityFrameworkCore;
using Moq;
using TaskDemo.Data;
using TaskDemo.Models;

namespace TaskDemoTest;

public class ToDoItemRepositoryTests
{
    [Fact]
    public void GetAllTasks_ReturnsAllTasks()
    {
        var repo = GetMockDataRepository();

        var toDos = repo.GetAll();
        Assert.Equal(4, toDos.Count());
    }

    [Fact]
    public void GetTaskById_ReturnTask()
    {
        var repo = GetMockDataRepository();
        var testId = 3;
        // Act
        var found = repo.GetById(testId);

        // Assert
        Assert.Equal(testId, found?.Id);
    }

    [Fact]
    public void AddTask_ShouldAddTask()
    {
        var repo = GetMockDataRepository();
        var initialToDoItemsCount = repo.GetAll().Count();
        var testToDo = new ToDoItem
        {
            Task = "Sample"
        };
        // Act
        var addedId = repo.Add(testToDo);
        var finalToDoItemsCount = repo.GetAll().Count();
        // Assert
        Assert.Equal(initialToDoItemsCount + 1, finalToDoItemsCount);
        Assert.NotEqual(0, addedId);
    }

    [Fact]
    public void RemoveTask_ShouldRemoveTask()
    {
        var repo = GetMockDataRepository();
        var testId = 3;
        var initialToDoItemsCount = repo.GetAll().Count();

        // Act
        repo.Delete(testId);
        var finalToDoItemsCount = repo.GetAll().Count();
        // Assert
        Assert.Equal(initialToDoItemsCount - 1, finalToDoItemsCount);
        Assert.Null(repo.GetById(testId));
    }

    [Fact]
    public void UpdateTask_ShouldUpdateTask()
    {
        var repo = GetMockDataRepository();
        var testId = 3;
        var testStr = "UpdateValue";

        // Act
        var found = repo.GetById(testId);
        if (found != null)
        {
            found.Task = testStr;
            // Assert
            var updated = repo.GetById(testId);
            Assert.Equal(testStr, updated?.Task);
        }
    }

    private ToDoItemRepository GetMockDataRepository()
    {
        var context = new Mock<AppDbContext>();
        var toDoItems = new List<ToDoItem>
        {
            new ToDoItem { Id = 1, Task = "ToDo1", IsDone = false },
            new ToDoItem { Id = 2, Task = "ToDo2", IsDone = false  },
            new ToDoItem { Id = 3, Task = "ToDo3", IsDone = false  },
            new ToDoItem { Id = 4, Task = "ToDo4", IsDone = false },
        };

        IQueryable<ToDoItem> toDoItemsQueryable = toDoItems.AsQueryable();
        var mockItems = new Mock<DbSet<ToDoItem>>();
        mockItems.As<IQueryable<ToDoItem>>().Setup(s => s.Provider).Returns(toDoItemsQueryable.Provider);
        mockItems.As<IQueryable<ToDoItem>>().Setup(s => s.Expression).Returns(toDoItemsQueryable.Expression);
        mockItems.As<IQueryable<ToDoItem>>().Setup(s => s.ElementType).Returns(toDoItemsQueryable.ElementType);
        mockItems.As<IQueryable<ToDoItem>>().Setup(s => s.GetEnumerator()).Returns(() => toDoItemsQueryable.GetEnumerator());
        mockItems.Setup(x => x.Add(It.IsAny<ToDoItem>())).Callback<ToDoItem>(t => {
            t.Id = toDoItems.Count;
            toDoItems.Add(t);
        });
        mockItems.Setup(x => x.Update(It.IsAny<ToDoItem>())).Callback<ToDoItem>(t =>
        {
            var found = toDoItems.SingleOrDefault(td => td.Id == t.Id);
            if (found != null)
            {
                found.IsDone = t.IsDone;
                found.Task = t.Task;
            }
        });
        mockItems.Setup(x => x.Remove(It.IsAny<ToDoItem>())).Callback<ToDoItem>(t =>
        {
            var found = toDoItems.SingleOrDefault(td => td.Id == t.Id);
            if (found != null)
            {
                toDoItems.Remove(t);
            }
        });
        context.Setup(c => c.ToDoItems).Returns(mockItems.Object);

        var repo = new ToDoItemRepository(context.Object);
        return repo;
    }
}
