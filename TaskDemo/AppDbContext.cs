using Microsoft.EntityFrameworkCore;
using TaskDemo.Models;

namespace TaskDemo.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base (dbContextOptions) 
		{
		}

		public AppDbContext()
		{

		}
		public virtual DbSet<ToDoItem> ToDoItems { get; set; }
	}
}

