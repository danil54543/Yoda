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

		/// <summary>
		/// Adding todo to database.
		/// </summary>
		/// <param name="entity">User.</param>
		public async Task Create(Todo entity)
		{
			await db.Todos.AddAsync(entity);
			await db.SaveChangesAsync();
		}
		/// <summary>
		/// Delete todo from database.
		/// </summary>
		/// <param name="entity">User.</param>
		public async Task Delete(Todo entity)
		{
			db.Todos.Remove(entity);
			await db.SaveChangesAsync();
		}

		/// <summary>
		/// Get all todos from database.
		/// </summary>
		public IQueryable<Todo> GetAll()
		{
			return db.Todos;
		}

		/// <summary>
		/// Updating todo in database.
		/// </summary>
		/// <param name="entity">User.</param>
		public async Task<Todo> Update(Todo entity)
		{
			db.Todos.Update(entity);
			await db.SaveChangesAsync();
			return entity;
		}
	}
}
