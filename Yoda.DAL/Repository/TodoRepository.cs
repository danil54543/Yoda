using Microsoft.EntityFrameworkCore;
using Yoda.DAL.Interface;
using Yoda.Domain.Model;

namespace Yoda.DAL.Repository
{
	/// <summary>
	/// TodoItem repository.
	/// </summary>
	public class TodoRepository : ITodoRepository
	{
		/// <summary>
		/// Database context instance.
		/// </summary>

		private readonly AppDbContext db;

		public TodoRepository(AppDbContext dbContext)
		{
			db = dbContext;
		}

		public async Task Create(Todo entity)
		{
			await db.Todos.AddAsync(entity);
			await db.SaveChangesAsync();
		}

		public async Task Delete(Todo entity)
		{
			db.Todos.Remove(entity);
			await db.SaveChangesAsync();
		}

		public IQueryable<Todo> GetAll()
		{
			return db.Todos;
		}

		public async Task<Todo> Update(Todo entity)
		{
			db.Todos.Update(entity);
			await db.SaveChangesAsync();
			return entity;
		}
	}
}
