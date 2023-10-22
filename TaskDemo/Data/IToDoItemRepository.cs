using TaskDemo.Models;

namespace TaskDemo.Data
{
	public interface IToDoItemRepository
	{
		int Add(ToDoItem toDoItem);
		void Update(ToDoItem toDoItem);
		bool Delete(int id);
		ToDoItem? GetById(int id);
		IEnumerable<ToDoItem> GetAll();
    }
}

