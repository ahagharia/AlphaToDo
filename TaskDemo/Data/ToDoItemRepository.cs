using TaskDemo.Models;

namespace TaskDemo.Data
{
	public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly AppDbContext _appDbContext;
        public ToDoItemRepository(AppDbContext appDbContext)
		{
            _appDbContext = appDbContext;
		}

        public int Add(ToDoItem toDoItem)
        {
            _appDbContext.ToDoItems.Add(toDoItem);
            _appDbContext.SaveChanges();
            return toDoItem.Id;
        }

        public void Update(ToDoItem toDoItem)
        {
            _appDbContext.ToDoItems.Update(toDoItem);
            _appDbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var item = GetById(id);
            if (item == null)
            {
                return false;
            }
            _appDbContext.ToDoItems.Remove(item);
            _appDbContext.SaveChanges();
            return true;
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            return _appDbContext.ToDoItems.ToList();
        }

        public ToDoItem? GetById(int id)
        {
            return _appDbContext.ToDoItems.FirstOrDefault(t => t.Id == id);
        }
    }
}

