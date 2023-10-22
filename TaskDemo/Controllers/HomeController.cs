using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskDemo.Data;
using TaskDemo.Models;

namespace TaskDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IToDoItemRepository _toDoItemRepository;

    public HomeController(ILogger<HomeController> logger,
        IToDoItemRepository toDoItemRepository)
    {
        _logger = logger;
        _toDoItemRepository = toDoItemRepository;
    }

    public IActionResult Index()
    {
        var toDoItems = _toDoItemRepository.GetAll();
        return View(toDoItems);
    }

    public IActionResult Add(ToDoItem toDoItem)
    {
        if (ModelState.IsValid)
        {
            _toDoItemRepository.Add(toDoItem);
            return RedirectToAction("Index");
        }
        return View(toDoItem);
    }

    public IActionResult Edit(int id)
    {
        var found = _toDoItemRepository.GetById(id);
        if (found == null)
        {
            return NotFound();
        }
        return View(found);
    }

    public IActionResult Update(ToDoItem toDoItem)
    {
        var found = _toDoItemRepository.GetById(toDoItem.Id);
        if (found == null)
        {
            return NotFound();
        }
        found.Task = toDoItem.Task;
        _toDoItemRepository.Update(found);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        if (!_toDoItemRepository.Delete(id))
        {
            return NotFound();
        }
        return RedirectToAction("Index");
    }
    public IActionResult ToggleIsDone (int id)
    {
        var found = _toDoItemRepository.GetById(id);
        if (found != null)
        {
            found.IsDone = !found.IsDone;
            _toDoItemRepository.Update(found);
        }
        return RedirectToAction("Index");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

